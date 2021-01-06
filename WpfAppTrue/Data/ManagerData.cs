using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace WpfAppTrue
{
    class ManagerData
    {
        public static string M_Surname;
        public static string M_Name;
        public static string M_Mail;
        public static string M_Domain;     //data used to creted a sql query 
        public static string M_password;
        public static string M_password2;
        public static string M_Group;
        public static string M_ID;


        public static void Datarowrequet(DataGrid DatagridName)
        {
            DataRowView row = DatagridName.SelectedItem as DataRowView;  // save the data from the selcted row in a datagrid
            if (row != null)
            {
         
                ManagerData.M_ID = row["ID"].ToString();
                ManagerData.M_Group = row["Group_Name"].ToString();
                ManagerData.M_Name = row["Name"].ToString();
                ManagerData.M_Mail = row["Mail"].ToString();
                ManagerData.M_Domain = row["Domain"].ToString();
            }

        }
    }
}
