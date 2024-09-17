using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficialTutorialPractice
{
    public partial class Login : Form
    {
        //Sql Command objects
        SqlConnection con;
        SqlCommand cmd;

        // Object for Register User Form
        RegisterUser registerUser = new RegisterUser();

        public Login()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["LocalDBConnectionStringReg"].ConnectionString;
            con = new SqlConnection(connectionString);
            //
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnLogin_KeyDown);
            //
            pictureBox1.Image = Properties.Resources.LoginImg;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text == "" && txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Username AND Password", "Login Failed!");
                    return;
                }
                else
                {
                    // Calling Stored Procedure
                    cmd = new SqlCommand("ValidateUser", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Add parameters to prevet SQL Injection
                    cmd.Parameters.AddWithValue("@NameUser", txtUser.Text);
                    cmd.Parameters.AddWithValue("@NamePass", txtPassword.Text);

                    con.Open();

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        MessageBox.Show("You have Successfully Logged In!");
                        Form1 obj = new Form1();
                        this.Hide();
                        obj.Show();
                    }
                    else 
                    {
                        MessageBox.Show("Please Check Username AND/OR Password", "Login Failed!");
                        ClearInfo();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Warning");
                ClearInfo();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public void ClearInfo()
        {
            txtPassword.Text = "";
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnLogin.PerformClick();
            }
        }

        private void btnRegUser_Click(object sender, EventArgs e)
        {
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
        }
    }
}
