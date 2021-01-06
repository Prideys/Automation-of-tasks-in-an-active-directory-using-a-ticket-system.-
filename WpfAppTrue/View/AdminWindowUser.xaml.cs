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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WpfAppTrue
{
    /// <summary>
    /// Interaction logic for AdminWindowUser.xaml
    /// </summary> //Background="#909090"
    public partial class AdminWindowUser : Window
    {
        public AdminWindowUser()
        {
            InitializeComponent();          
            Sql_Connection.FillDataGridUser(NewUser);
            
        }
    
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Open_User_validation(object sender, RoutedEventArgs e)
        {
            AdminWindow mainWindow = new AdminWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Button_Validate(object sender, RoutedEventArgs e)
        {           
            AdminData.DatarowUser(NewUser);
            Sql_Connection.UPdateDataNewUser(AdminData.U_Name);         
            Powershell.Take_Serveur_Name(AdminData.U_Name);
            Powershell.Take_password();
            Powershell.Create_User(AdminData.U_Serveur_Name, AdminData.U_Surname,AdminData.U_Name, AdminData.U_Name, AdminData.U_password_decrypted);
            Sql_Connection.FillDataGridUser(NewUser);
        }

        private void Button_Reject(object sender, RoutedEventArgs e)
        {
            AdminData.DatarowUser(NewUser);
            Sql_Connection.DeleteDataNewUser(AdminData.U_Name);
            Sql_Connection.FillDataGridUser(NewUser);
          
        }
    }
}
