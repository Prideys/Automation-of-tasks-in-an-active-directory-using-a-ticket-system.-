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
    class AdminData
    {
        public static string A_ID;
        public static string A_Group;
        public static string A_Name;    //data used to creted a sql query 
        public static string A_Mail;
        public static string A_Domain;


        public static string U_Name;      
        public static string U_Domain;
        public static string U_Surname;     //data used to creted a sql query 
        public static string U_Serveur_Name;
        public static string U_password;
        public static string U_password_decrypted;

        public static void Datarowrequet(DataGrid DatagridName) // save the data from the selcted row in a datagrid
        {
            DataRowView row = DatagridName.SelectedItem as DataRowView;
            if (row != null)
            {
                AdminData.A_ID = row["ID"].ToString();
                AdminData.A_Group = row["Group_Name"].ToString();
                AdminData.A_Name = row["Name"].ToString();
                AdminData.A_Mail = row["Mail"].ToString();
                AdminData.A_Domain = row["Domain"].ToString();
             
            }
        }

        public static void DatarowUser(DataGrid DatagridName)  // save the data from the selcted row in a datagrid
        {
            DataRowView row = DatagridName.SelectedItem as DataRowView;
            if (row != null)
            {
                AdminData.U_Name = row["Name"].ToString();
                AdminData.U_Surname = row["Surname"].ToString();
                AdminData.U_Domain = row["Domain"].ToString();
            }
        }
    }
}


/*
 * 
 * 
 * 
 * */