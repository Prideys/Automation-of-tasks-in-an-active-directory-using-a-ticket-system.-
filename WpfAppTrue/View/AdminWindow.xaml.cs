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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
          
            Sql_Connection.FillDataGridRequest(grdRequest); 
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
            AdminWindowUser mainWindow = new AdminWindowUser();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            AdminData.Datarowrequet(grdRequest);
            Sql_Connection.UPdateDataRequest(AdminData.A_ID);
            Sql_Connection.FillDataGridRequest(grdRequest);
            Powershell.Take_Serveur_Name(AdminData.A_Name);
            Powershell.Add_User_IntoGroups(AdminData.U_Serveur_Name, AdminData.A_Group, AdminData.A_Name);

        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            AdminData.Datarowrequet(grdRequest);
            Sql_Connection.DeleteDataNewRequest(AdminData.A_ID);
            Sql_Connection.FillDataGridRequest(grdRequest);
            
        }
      

        private void GrdRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}