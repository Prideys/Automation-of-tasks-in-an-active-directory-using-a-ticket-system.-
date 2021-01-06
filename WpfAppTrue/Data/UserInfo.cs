using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Controls;
using System.Windows;

namespace WpfAppTrue
{
    class UserInfo
    {
        public static String CustomerName = "";
        public static String CustomerEmail = "";
        public static String CustomerRole = "";
        public static String CustomerID = "";  // Data used for save request into the database (basic information from the user connected)
        public static String PasswordE = "";
        public static String DomainID = "";

        public static string combobox_text = "";
        public static string justi_text = "";
        public static int GroupID;

       


    }
}
