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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WpfAppTrue
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            
            Sql_Connection.FillDataGridRequestManager(UserAproval);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
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

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
           
            ManagerWindow_NewRequest mainWindow = new ManagerWindow_NewRequest();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            ManagerData.Datarowrequet(UserAproval);
            Sql_Connection.UPdateDataRequestManager(ManagerData.M_ID);
            Sql_Connection.FillDataGridRequestManager(UserAproval);
            
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            ManagerData.Datarowrequet(UserAproval);
            Sql_Connection.DeleteDataNewRequest(ManagerData.M_ID);
            Sql_Connection.FillDataGridRequestManager(UserAproval);
        }
    }
}
