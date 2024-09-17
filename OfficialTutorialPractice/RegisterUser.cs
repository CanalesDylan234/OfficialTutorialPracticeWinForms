using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace OfficialTutorialPractice
{
    public partial class RegisterUser : Form
    {

        //Sql Command objects
        SqlConnection con;
        SqlCommand cmd;

        public RegisterUser()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["LocalDBConnectionStringReg"].ConnectionString;
            con = new SqlConnection(connectionString);

            //
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRegister_KeyDown);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //
            if (txtNewUser.Text == "" || txtNewPass.Text == "")
            {
                MessageBox.Show("Please Enter Username AND Password", "Login Failed!");
                return;
            }

            try
            {
                //
                con.Open();

                //
                cmd = new SqlCommand("RegisterUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //
                cmd.Parameters.AddWithValue("@UsersName", txtNewUser.Text);
                cmd.Parameters.AddWithValue("@UsersPass", txtNewPass.Text);

                //
                cmd.ExecuteNonQuery();

                // 
                MessageBox.Show("Registration Successful!", "Registration Complete!");
            }
            catch (SqlException ex)
            {
                // Handle the specific SQL Exception called within the stored procedure 
                // 50000 used to signaling that this is a user-defined error rather than a system-generated one
                if (ex.Number == 50000)
                {
                    MessageBox.Show("Username already exists. Please choose a different one.", "Registration Failed");
                }
                else
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Registration Failed");
                }
            }
            finally
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void btnRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister.PerformClick();
            }
        }
    }
}
