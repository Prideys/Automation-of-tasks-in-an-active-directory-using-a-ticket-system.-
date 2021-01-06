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
using System.Data.SqlClient;
using System.Security.Cryptography;
using WpfAppTrue;

namespace WpfAppTrue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

       
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            Sql_Connection.Login(Loginbox, Passwordbox);
            if (Sql_Connection.message == "1")
            {
                if (UserInfo.CustomerRole == "User")
                {
                    UserWindows mainWindow = new UserWindows();
                    mainWindow.Top = this.Top;
                    mainWindow.Left = this.Left;
                    mainWindow.Show();
                    this.Close();
                }
                else if (UserInfo.CustomerRole == "Admin")
                {
                    AdminWindow mainWindow = new AdminWindow();
                    mainWindow.Top = this.Top;
                    mainWindow.Left = this.Left;
                    mainWindow.Show();
                    this.Close();

                }
                else if (UserInfo.CustomerRole == "Manager")
                {
                    ManagerWindow mainWindow = new ManagerWindow();
                    mainWindow.Top = this.Top;
                    mainWindow.Left = this.Left;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No rigth, access prohibited", "Info");
                }

            }
            else
                MessageBox.Show(Sql_Connection.message, "Info");
        }

        private void Loginbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


