using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.IO;



namespace WpfAppTrue
{
    class Sql_Connection
    {
        public static String message = "Invalid Credentials"; //Display if Invalid credential 
        public static SqlConnection conn = new SqlConnection(@"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test");

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


        }  // bind a combobox whith Group Name depending on the domain

        public static void BindComboBoxuser(ComboBox comboBoxName)
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from Credential where Domain_ID=@UserDomain", Sql_Connection.conn);
            da.SelectCommand.Parameters.AddWithValue("@UserDomain", UserInfo.DomainID);
            DataSet ds = new DataSet();

            da.Fill(ds, "Credential");

            //Binding the data to the combobox.
            comboBoxName.ItemsSource = ds.Tables["Credential"].DefaultView;


            //To display category name (DisplayMember in Visual Studio 2005)
            comboBoxName.DisplayMemberPath = "Mail";

            //To store the ID as hidden (ValueMember in Visual Studio 2005)
            comboBoxName.SelectedValuePath = "Name";
            comboBoxName.SelectedValuePath = "ID";
            comboBoxName.SelectedValuePath = "Password";
            comboBoxName.SelectedValuePath = "Role";
            comboBoxName.SelectedValuePath = "Domain_id";
            comboBoxName.SelectedValuePath = "Exists_";
            comboBoxName.SelectedValuePath = "Surname";


        } //Bind a combobox whith User Name depending on the domain

        public static void FillDataGridUser(DataGrid DatagridName)

        {
            SqlConnection con;
            SqlCommand cmd;
           
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            string CmdString = string.Empty;


            con = new SqlConnection(connectionString);
            CmdString = @" SELECT Credential.Mail, Credential.Name,Credential.Surname,Domain.Domain From Credential
                           INNER JOIN Domain ON Domain.ID = Credential.Domain_id
                           WHERE Credential.Exists_ = 'False'";
            cmd = new SqlCommand(CmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            DatagridName.ItemsSource = dt.DefaultView;
            cmd.Dispose();
            con.Close();



        }  //fill a datagrid whith user needed to be cretated

        public static void Login(TextBox TextBox,PasswordBox passwordbox)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("Select * from dbo.Credential where Mail=@CustomerEmail", con);
                cmd.Parameters.AddWithValue("@CustomerEmail", TextBox.Text.ToString());
                reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    string plain;

                    
                  // plain = passwordbox.Password.ToString();  //a effacer
                   

                  string encryptedString = Cryption.EncryptString(Cryption.key, passwordbox.Password.ToString());  // encrypted the password
                  plain = encryptedString;

                    if (reader["Password"].ToString().Equals(plain, StringComparison.InvariantCulture))
                    {
                        message = "1"; //Used for the knowing if the password are the same
                        UserInfo.CustomerEmail = TextBox.Text.ToString();  // registers the different user data
                        UserInfo.CustomerName = reader["Name"].ToString();
                        UserInfo.CustomerRole = reader["Role"].ToString();
                        UserInfo.CustomerID = reader["ID"].ToString();
                        UserInfo.DomainID = reader["Domain_id"].ToString();
                    }
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
            }


        }  //Verifiyed the login information 

        public static void FillDataGridRequest(DataGrid DatagridName)
        {
            SqlConnection con;
            SqlCommand cmd;
            
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            string CmdString = string.Empty;


            con = new SqlConnection(connectionString);
            CmdString = @"SELECT Request.ID ,Groups.Group_Name, Credential.Mail, Credential.Name, Domain.Domain  From Request
                              INNER JOIN Credential ON Credential.ID = User_ID
                              INNER JOIN Domain ON Domain.ID = Credential.Domain_id
                              INNER JOIN Groups ON Groups.ID = Group_ID
                              WHERE Request.Approval = 'True' AND Request.Done = 'False'";
            cmd = new SqlCommand(CmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            DatagridName.ItemsSource = dt.DefaultView;
            cmd.Dispose();
            con.Close();
        }  //fill a datagrid whith Request needed to be cretated for Admin

        public static void FillDataGridRequestManager(DataGrid DatagridName)
        {
            SqlConnection con;
            SqlCommand cmd;
           
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            string CmdString = string.Empty;


            con = new SqlConnection(connectionString);
            CmdString = @" SELECT Groups.Group_Name, Credential.Mail, Credential.Name, Request.ID, Groups.Description   From Request
                           INNER JOIN Credential ON Credential.ID = User_ID
                           INNER JOIN Groups ON Groups.ID = Group_ID
                           WHERE Credential.Domain_id = '1' AND Request.Done = 'False' AND Request.Approval = 'False'";
            cmd = new SqlCommand(CmdString, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            DatagridName.ItemsSource = dt.DefaultView;
            cmd.Dispose();
            con.Close();
        } //fill a datagrid whith user needed to be cretated for Manager

        public static void Selctionmodification(ComboBox ComboboxName, TextBox Textboxname)
        {

            Sql_Connection.conn.Open();
            SqlCommand cmd = new SqlCommand("select Description from Groups where Group_Name=@GroupName", Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@GroupName", ComboboxName.Text);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Textboxname.Text = dr.GetString(0).ToString();
            }
            dr.Close();
            SqlCommand cmd2 = new SqlCommand("select ID from Groups where Group_Name=@GroupName", Sql_Connection.conn);
            cmd2.Parameters.AddWithValue("@GroupName", ComboboxName.Text);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                UserInfo.GroupID = dr2.GetInt32(0);
            }
            Sql_Connection.conn.Close();
        }    // Modifing a textbox depending on what are selected on a combobox

        public static void ASK_request(TextBox Textboxname)
        {

            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            UserInfo.justi_text = Textboxname.Text;
            int UID = Int32.Parse(UserInfo.CustomerID);
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("Select * from dbo.Request where ID=@IDuser", con);
            cmd.Parameters.AddWithValue("@IDuser", UserInfo.CustomerID.ToString());
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string plain;


                plain = UserInfo.GroupID.ToString(); ;


                if (reader["Group_ID"].ToString().Equals(plain, StringComparison.InvariantCulture))
                {

                    MessageBox.Show("Request already Done", "Info");

                }
                else
                {
                    Sql_Connection.conn.Open();
                    string insert_query = ("INSERT INTO Request (User_ID, Group_ID, Done, Approval,Justification) VALUES (@UID,@GID,0,0,@justification)");
                    SqlCommand cmd2 = new SqlCommand(insert_query, Sql_Connection.conn);
                    cmd2.Parameters.AddWithValue("@UID", UID);
                    cmd2.Parameters.AddWithValue("@GID", UserInfo.GroupID);
                    cmd2.Parameters.AddWithValue("@justification", UserInfo.justi_text);
                    cmd2.ExecuteNonQuery();
                    try
                    {
                        MessageBox.Show("Request Done ", "Info");

                    }
                    catch
                    {
                        MessageBox.Show(UID + "    " + UserInfo.GroupID + "   " + UserInfo.justi_text, "Info");
                    }
                    Sql_Connection.conn.Close();
                }
            }
            

        }  //Methode use dor created the query saving a request

        public static void ASK_requestManager()
        {

               SqlConnection con;
               SqlCommand cmd;
               SqlDataReader reader;
               String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
                int UID = Int32.Parse(UserInfo.CustomerID);
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("Select * from dbo.Request where ID=@IDuser", con);
                cmd.Parameters.AddWithValue("@IDuser", UserInfo.CustomerID.ToString());
                reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    string plain;
                    

                    plain = UserInfo.GroupID.ToString(); ;


                    if (reader["Group_ID"].ToString().Equals(plain, StringComparison.InvariantCulture))
                    {

                       MessageBox.Show("Request already Done", "Info");

                    }
                    else
                    {
                    Sql_Connection.conn.Open();
                    string insert_query = ("INSERT INTO Request (User_ID, Group_ID, Done, Approval,Justification) VALUES (@UID,@GID,0,1,'Create by Manager')");
                    SqlCommand cmd2 = new SqlCommand(insert_query, Sql_Connection.conn);
                    cmd2.Parameters.AddWithValue("@UID", UID);
                    cmd2.Parameters.AddWithValue("@GID", UserInfo.GroupID);
                    cmd2.ExecuteNonQuery();
                    try
                    {
                        MessageBox.Show("Request Done ", "Info");

                    }
                    catch
                    {
                        MessageBox.Show(UID + "    " + UserInfo.GroupID + "   " + UserInfo.justi_text, "Info");
                    }
                    Sql_Connection.conn.Close();
                }
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();



        } //Methode use dor created the query saving a request for manager

        public static void ChangeUserRequest(ComboBox ComboboxName)
        {
            SqlDataReader reader;
            ComboboxName.Text = UserInfo.CustomerEmail;
            Sql_Connection.conn.Open();
            SqlCommand cmd = new SqlCommand("select ID from Credential where MAIL=@mail");
            cmd.Parameters.AddWithValue("@mail", (ComboboxName.Text));
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserInfo.CustomerID = reader["ID"].ToString();

            }

        } //CHanged the ID of teh request to allowing manager to do request fo other poeple.


        public static void UPdateDataNewUser(String Name)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("UPDATE Credential SET Exists_=1  where Name=@Nameuser");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@Nameuser", Name);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User create Done ", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();

        }  //Set a user in the database to created

        public static void DeleteDataNewUser(String Name)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("DELETE FROM Credential WHERE Name=@Nameuser");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@Nameuser", Name);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User Deleted", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        }  //deleted the user form the database

        public static void UPdateDataRequest(String ID)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("UPDATE Request SET Done=1  where ID=@ID");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User create Done ", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        }  //set a request on the database to created

        public static void DeleteDataNewRequest(String ID)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("DELETE FROM Request WHERE ID=@ID");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("ID Deleted", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        } //deleted request into the database

        public static void UPdateDataRequestManager(String ID)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("UPDATE Request SET Approval=1  where ID=@ID");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User create Done ", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        } //Update request into the database , approval from Manager

        public static void NewUser(string Mail, string Name, string Password, string Surname, string Domaine_Name,string role)
        {
            int Domain = Int32.Parse(Domaine_Name);
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            String connectionString = @"Data Source=L-LR0AXESR\SQLEXPRESS;Initial Catalog=WEB;User ID=Test;Password=Test";
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("Select * from dbo.Credential where Mail=@IDomain", con);
            cmd.Parameters.AddWithValue("@IDomain", Mail);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string plain;


                plain = Mail;


                if (reader["Mail"].ToString().Equals(plain, StringComparison.InvariantCulture))
                {

                    MessageBox.Show("User Alredy exist", "Info");
                    reader.Close();
                    reader.Dispose();
                    cmd.Dispose();
                    con.Close();
                }
              
            }
            else
            {
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();
                Sql_Connection.conn.Open();
                string insert_query = ("INSERT INTO Credential (Mail,Name, Password,Role,Domain_ID,Exists_,Surname) VALUES (@Mail,@Name,@Password,@role,@Domain,0,@Surname)");
                SqlCommand cmd2 = new SqlCommand(insert_query, Sql_Connection.conn);
                cmd2.Parameters.AddWithValue("@Mail", Mail);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Password", Password);
                cmd2.Parameters.AddWithValue("@Domain", Domain);
                cmd2.Parameters.AddWithValue("@Surname", Surname);
                cmd2.Parameters.AddWithValue("@role", role);
                cmd2.ExecuteNonQuery();
                try
                {
                    MessageBox.Show("Request Done ", "Info");
                    Sql_Connection.conn.Close();
                }
                catch
                {
                    MessageBox.Show("Error  ", "Info");
                    Sql_Connection.conn.Close();
                }

            }
            
        } //Query created new user

        public static void UpgradeUser(ComboBox Name)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("UPDATE Credentail SET Role=Manager  where Name=@Name");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@Name", Name.Text);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User Updated ", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        } //Upgrade user into database user to manager

        public static void DeletedUser(ComboBox Name)
        {
            Sql_Connection.conn.Open();
            string insert_query = ("DELETE FROM Credential WHERE Name=@Name;");
            SqlCommand cmd = new SqlCommand(insert_query, Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@Name", Name.Text);
            cmd.ExecuteNonQuery();
            try
            {
                MessageBox.Show("User Deleted ", "Info");

            }
            catch
            {
                MessageBox.Show("Error", "Info");
            }
            Sql_Connection.conn.Close();
        } //deleted user into the database


    }
}
