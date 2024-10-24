using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aibolit.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(((MainWindow)Application.Current.MainWindow).ServerName))
                ServerNameSetting.Text = ((MainWindow)Application.Current.MainWindow).ServerName;

            if (!string.IsNullOrEmpty(((MainWindow)Application.Current.MainWindow).ServerPort))
                PortServerSetting.Text = ((MainWindow)Application.Current.MainWindow).ServerPort;

            if (!string.IsNullOrEmpty(((MainWindow)Application.Current.MainWindow).DatabaseName))
                DatabaseNameSetting.Text = ((MainWindow)Application.Current.MainWindow).DatabaseName;

            if (!string.IsNullOrEmpty(((MainWindow)Application.Current.MainWindow).UserName))
                UserNameSetting.Text = ((MainWindow)Application.Current.MainWindow).UserName;

            if (!string.IsNullOrEmpty(((MainWindow)Application.Current.MainWindow).UserPassword))
                UserPasswordSetting.Text = ((MainWindow)Application.Current.MainWindow).UserPassword;
        }

        private void SaveSettingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ServerNameSetting.Text))
                ((MainWindow)Application.Current.MainWindow).ServerName = ServerNameSetting.Text;

            if (!string.IsNullOrEmpty(PortServerSetting.Text))
                ((MainWindow)Application.Current.MainWindow).ServerPort = PortServerSetting.Text;

            if (!string.IsNullOrEmpty(DatabaseNameSetting.Text))
                ((MainWindow)Application.Current.MainWindow).DatabaseName = DatabaseNameSetting.Text;

            if (!string.IsNullOrEmpty(UserNameSetting.Text))
                ((MainWindow)Application.Current.MainWindow).UserName = UserNameSetting.Text;

            if (!string.IsNullOrEmpty(UserPasswordSetting.Text))
                ((MainWindow)Application.Current.MainWindow).UserPassword = UserPasswordSetting.Text;
        }
    }
}
