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
    // Faire les button pour delete ou upgrade user.
    public partial class ManagerWindow_NewRequest : Window
    {
        public ManagerWindow_NewRequest()
        {
            InitializeComponent();
            Sql_Connection.BindComboBox(Group_request);
            Sql_Connection.BindComboBoxuser(Change_request);
            Sql_Connection.BindComboBoxuser(Upgrade_user);
            Sql_Connection.BindComboBoxuser(Delete_user);
           
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void ApprouveGroup_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow mainWindow = new ManagerWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow_NewUser mainWindow = new ManagerWindow_NewUser();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Change_user_request(object sender, SelectionChangedEventArgs e)
        {
            Sql_Connection.ChangeUserRequest(Change_request);        
        }

        private void Group_add_description(object sender, SelectionChangedEventArgs e)
        {
            Sql_Connection.Selctionmodification(Group_request, group_Description);
           
        }   

        private void Do_Request(object sender, RoutedEventArgs e)
        {
            Sql_Connection.ASK_requestManager();
          
        }

        private void Upgrade_User(object sender, RoutedEventArgs e)
        {
            Sql_Connection.UpgradeUser(Upgrade_user);
        }
       
        private void Deleted_User(object sender, RoutedEventArgs e)
        {
            Sql_Connection.DeletedUser(Delete_user);
            Powershell.Take_Serveur_Name(ManagerData.M_Name);
            Powershell.Delete_User(AdminData.U_Serveur_Name, ManagerData.M_Name);
            
        }
    }

}
