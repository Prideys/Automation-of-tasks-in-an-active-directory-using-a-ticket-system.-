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

namespace WpfAppTrue
{
    public partial class ManagerWindow_NewUser : Window
    {
        public ManagerWindow_NewUser()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void ApprouveUser_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow mainWindow = new ManagerWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

     

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {

            ManagerWindow_NewRequest mainWindow = new ManagerWindow_NewRequest();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        
        
         private void New_User(object sender, RoutedEventArgs e)
         {

               ManagerData.M_Domain = UserInfo.DomainID;
               ManagerData.M_Mail = Mail_texybox.Text;
               ManagerData.M_Surname = Surname_texybox.Text;
               ManagerData.M_password = Password_texybox.Text;
              
               ManagerData.M_Name = Name_texybox.Text;
               ManagerData.M_password2 = Password2_texybox.Text;
                string role ="User";
             if (ManagerData.M_password == ManagerData.M_password2)
             {
                string encryptedString = Cryption.EncryptString(Cryption.key, ManagerData.M_password);
                string plain2 = encryptedString;

                Sql_Connection.NewUser(ManagerData.M_Mail, ManagerData.M_Name, plain2, ManagerData.M_Surname, ManagerData.M_Domain, role);

            }
            else 
            {

                MessageBox.Show("Password are not the Same ", "Info");

            }
        }
    }
}
