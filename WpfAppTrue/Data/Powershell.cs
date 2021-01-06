using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Windows;
using System.Data;

namespace WpfAppTrue
{
    class Powershell
    {
        public static void Add_User_IntoGroups(string Serveur,string GroupName,string UserName)
        {

            Runspace runSpace = RunspaceFactory.CreateRunspace();

            Pipeline pipeline = runSpace.CreatePipeline();

            runSpace.Open();

            pipeline.Commands.AddScript("$sessions = New-PSSession -ComputerName" + Serveur + "" + Environment.NewLine
                + "Invoke-Command -session $sessions -ScriptBlock {Import-Module ActiveDirectory}" + Environment.NewLine
              + "Invoke-Command -session $sessions -ScriptBlock {Add-ADGroupMember -Identity " + GroupName + " -Members " + UserName + "}" + Environment.NewLine
              + "Remove-PSSession -Session $sessions");



            Collection<PSObject> results = pipeline.Invoke();
            MessageBox.Show("Request is Done");            

        }  //  methode created Powershell cmd add user into a AD group

        public static void Delete_User(string Serveur, string UserName)
        {

            Runspace runSpace = RunspaceFactory.CreateRunspace();

            Pipeline pipeline = runSpace.CreatePipeline();

            runSpace.Open();

            pipeline.Commands.AddScript("$sessions = New-PSSession -ComputerName" + Serveur + "" + Environment.NewLine
                + "Invoke-Command -session $sessions -ScriptBlock {Import-Module ActiveDirectory}" + Environment.NewLine
              + "Invoke-Command -session $sessions -ScriptBlock {Disable-ADAccount -Identity " + UserName + "}" + Environment.NewLine
              + "Remove-PSSession -Session $sessions");



            Collection<PSObject> results = pipeline.Invoke();
            MessageBox.Show("Request is Done");

        } //  methode created Powershell cmd deleted User (disable it )

        public static void Create_User(string Serveur, string Surname, string UserName,string Pname,string password)
        {

            Runspace runSpace = RunspaceFactory.CreateRunspace();

            Pipeline pipeline = runSpace.CreatePipeline();

            runSpace.Open();

            pipeline.Commands.AddScript("$sessions = New-PSSession -ComputerName" + Serveur + "" + Environment.NewLine
                + "Invoke-Command -session $sessions -ScriptBlock {Import-Module ActiveDirectory}" + Environment.NewLine
              + "Invoke-Command -session $sessions -ScriptBlock {New-ADUser -Name " + UserName + " -GivenName " + UserName + " -Surname " + Surname + " -UserPrincipalName " + Pname + " -path \"OU=User,OU=Office,DC=ep,DC=local\" -AccountPassword(ConvertTo-SecureString "+ password + " -AsPlainText -Force)}" + Environment.NewLine
               + "Invoke-Command -session $sessions -ScriptBlock {Enable-ADAccount -Identity " + UserName + "}" + Environment.NewLine
              + "Remove-PSSession -Session $sessions");



            Collection<PSObject> results = pipeline.Invoke();
            MessageBox.Show("Request is Done");

        }   //  methode created Powershell cmd to creted user
        public static void Take_Serveur_Name (string Name)
        {
            Sql_Connection.conn.Open();
            SqlCommand cmd = new SqlCommand("select Serveur from Domain where Domain=@ID", Sql_Connection.conn);
            cmd.Parameters.AddWithValue("@GID", Name);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                AdminData.U_Serveur_Name = dr.GetString(0).ToString();
            }
            dr.Close();         
            Sql_Connection.conn.Close();


        }  //  Take the server name from the ID 
        public static void Take_password()
        {
            
            Sql_Connection.conn.Open();
            SqlCommand cmd2 = new SqlCommand("select Password from Credential where Name=@Name", Sql_Connection.conn);
            cmd2.Parameters.AddWithValue("@GName", AdminData.U_Name);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                AdminData.U_password = dr2.GetString(0).ToString();
            }
            dr2.Close();
            AdminData.U_password_decrypted = Cryption.EncryptString(Cryption.key, AdminData.U_password.ToString());           
            Sql_Connection.conn.Close();
        }   //  Take the password fo the new user from the database


    }
}
