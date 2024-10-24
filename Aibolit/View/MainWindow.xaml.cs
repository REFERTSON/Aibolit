using Aibolit.View;
using System.Windows;
using System.Windows.Controls;

namespace Aibolit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Настройки
        public string ServerName { get; set; } = "localhost";
        public string ServerPort { get; set; } = "5432";
        public string DatabaseName { get; set; } = "Aibolit";
        public string UserName { get; set; } = "postgres";
        public string UserPassword { get; set; } = "123";

        // Запросы для таблиц
        public string ClientsTableQuery = "SELECT * FROM public.\"Clients\"";
        public string OrdersTableQuery = "SELECT * FROM public.\"Orders\"";
        public string EmployeesTableQuery = "SELECT * FROM public.\"Employees\"";
        public string MaterialsTableQuery = "SELECT * FROM public.\"Materials\"";
        public string WorksTableQuery = "SELECT * FROM public.\"Works\"";


        private DatabaseManager databaseManager { get; set; }
        private TableManager tableManager { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
            tableManager = new TableManager();
        }

        private void ClientsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new TableView(databaseManager, tableManager, "Клиенты", ClientsTableQuery, $"Server={ServerName};Port={ServerPort};Database={DatabaseName}; User Id={UserName}; Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void OrdersRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new TableView(databaseManager, tableManager, "Заказы", OrdersTableQuery, $"Server={ServerName};Port={ServerPort};Database={DatabaseName};User Id={UserName};Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void EmployeesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new TableView(databaseManager, tableManager, "Работники", EmployeesTableQuery, $"Server={ServerName};Port={ServerPort};Database={DatabaseName};User Id={UserName};Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void MaterialsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new TableView(databaseManager, tableManager, "Материалы", MaterialsTableQuery, $"Server={ServerName};Port={ServerPort};Database={DatabaseName};User Id={UserName};Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void WorksRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new TableView(databaseManager, tableManager, "Работы", WorksTableQuery, $"Server={ServerName};Port={ServerPort};Database={DatabaseName};User Id={UserName};Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void ReportsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new ReportsView(databaseManager, tableManager, $"Server={ServerName};Port={ServerPort};Database={DatabaseName};User Id={UserName};Password={UserPassword};"));
            SettingsRadioButton.IsChecked = false;
        }

        private void SettingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FramePage.Navigate(new SettingsView());
            MenuStackPanel.Children.OfType<RadioButton>().ToList().ForEach(r => r.IsChecked = false);
        }
    }
}