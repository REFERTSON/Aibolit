using Microsoft.Win32;
using MiniExcelLibs;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Aibolit.View
{
    /// <summary>
    /// Логика взаимодействия для TableView.xaml
    /// </summary>
    public partial class TableView : Page
    {
        private string Query { get; set; }
        private string ConnectionString { get; set; } = "";

        private TableManager _tableManager { get; set; }
        private DatabaseManager _databaseManager { get; set; }
        private DataTable _table { get; set; }
        private OpenFolderDialog dialog { get; set; }

        public TableView(DatabaseManager databaseManager, TableManager tableManager, string tableName, string query, string connectionString)
        {
            Query = query;
            ConnectionString = connectionString;

            _tableManager = tableManager;
            _databaseManager = databaseManager;
            _table = new DataTable();

            dialog = new OpenFolderDialog();
            dialog.Multiselect = false;
            dialog.Title = "Выберете папку для сохранения";

            InitializeComponent();

            TableNameLabel.Content = tableName;

            UpdateTable();
        }

        private void UpdateTable()
        {
            _databaseManager.CreateConnection(ConnectionString);
            _tableManager.RunSqlQuery(_databaseManager.GetSqlConnection(), Query, _table);
            _tableManager.FillDataGridView(DataGridTable, _table);
        }

        private void UpdateTableButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void ExportTableToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            bool? result = dialog.ShowDialog();

            if (result == true && DataGridTable.ItemsSource != null)
            {
                try
                {
                    MiniExcel.SaveAs(Path.Combine(dialog.FolderName, $"{TableNameLabel.Content}.xlsx"), ((DataView)DataGridTable.ItemsSource).ToTable(), sheetName: $"{TableNameLabel.Content}", overwriteFile: true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось экспортировать таблицу в Excel! Текст ошибки: {ex.Message}", "Ошибка экспорта в Excel", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не удалось экспортировать таблицу в Excel!", "Ошибка экспорта в Excel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
