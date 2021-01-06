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
    /// Interaction logic for ManagerWindow_au.xaml
    /// </summary>
    public partial class ManagerWindow_au : Window
    {
        public ManagerWindow_au()
        {
            InitializeComponent();
        }

        public static void BindComboBox(ComboBox comboBoxName)
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from Groups where Domain_ID=@UserDomain", Sql_Connection.conn);
            da.SelectCommand.Parameters.AddWithValue("@UserDomain", UserInfo.DomainID);
            DataSet ds = new DataSet();

            da.Fill(ds, "Groups");

            //Binding the data to the combobox.
            comboBoxName.ItemsSource = ds.Tables["Groups"].DefaultView;


            //To display category name (DisplayMember in Visual Studio 2005)
            comboBoxName.DisplayMemberPath = "Group_Name";

            //To store the ID as hidden (ValueMember in Visual Studio 2005)
            comboBoxName.SelectedValuePath = "Type";
            comboBoxName.SelectedValuePath = "ID";
            comboBoxName.SelectedValuePath = "Domain_ID";
            comboBoxName.SelectedValuePath = "Description";


        }
    }
}
