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
    /// Interaction logic for UserWindows.xaml
    /// </summary>
    public partial class UserWindows : Window
    {
        public UserWindows()
        {
            InitializeComponent();
            Sql_Connection.BindComboBox(group_combo);
                               
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Top = this.Top;
            mainWindow.Left = this.Left;
            mainWindow.Show();
            this.Close();
        }

        private void Do_Request(object sender, RoutedEventArgs e)
        {
            Sql_Connection.ASK_request(group_Justi);         
        }
        
     

        private void Group_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sql_Connection.Selctionmodification(group_combo, group_Description);          
        }
    }
}
