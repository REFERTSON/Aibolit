using Microsoft.Win32;
using MiniExcelLibs;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Aibolit.View
{
    /// <summary>
    /// Логика взаимодействия для ReportsView.xaml
    /// </summary>
    public partial class ReportsView : Page
    {

        private string ConnectionString { get; set; } = "";

        private TableManager _tableManager { get; set; }
        private DatabaseManager _databaseManager { get; set; }
        private DataTable _table { get; set; }
        private OpenFolderDialog dialog { get; set; }

        private IList<string> QueriesList { get; } = new List<string>()
        {
            "SELECT \"Full_Name\", \"Description\" FROM public.\"Clients\" C LEFT JOIN public.\"Orders\" O ON O.\"ID_Client\" = C.\"ID_Client\";",
            "SELECT \"Full_Name\", \"Title\" FROM public.\"Employees\" E RIGHT JOIN public.\"Works\" W ON W.\"ID_Employee\" = E.\"ID_Employee\" ORDER BY \"ID_Work\" ASC;",
            "SELECT \"ID_Client\" FROM public.\"Orders\" WHERE \"Cost\" > 15000;",
            "SELECT * FROM public.\"Orders\" ORDER BY \"Date\" DESC LIMIT 5 OFFSET 2;",
            "SELECT * FROM public.\"Orders\" WHERE \"ID_Client\" IN (1, 3, 5);"
        };


        public ReportsView(DatabaseManager databaseManager, TableManager tableManager, string connectionString)
        {
            ConnectionString = connectionString;

            _tableManager = tableManager;
            _databaseManager = databaseManager;

            dialog = new OpenFolderDialog();
            dialog.Multiselect = false;
            dialog.Title = "Выберете папку для сохранения";

            InitializeComponent();
        }

        private void UpdateTableButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _table = new DataTable();
            _databaseManager.CreateConnection(ConnectionString);
            _tableManager.RunSqlQuery(_databaseManager.GetSqlConnection(), QueriesList[Queries.SelectedIndex], _table);
            _tableManager.FillDataGridView(DataGridTable, _table);
        }
            

        private void ExportTableToExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool? result = dialog.ShowDialog();

            if (result == true && DataGridTable.ItemsSource != null)
            {
                try
                {
                    MiniExcel.SaveAs(Path.Combine(dialog.FolderName, $"{((ComboBoxItem)Queries.SelectedItem).Content.ToString()}.xlsx"), _table, sheetName: $"Отчет", overwriteFile: true);
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
