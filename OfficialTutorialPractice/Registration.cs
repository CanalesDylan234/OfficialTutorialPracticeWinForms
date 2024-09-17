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
using System.Dynamic;
using Microsoft.Office.Interop.Excel;

namespace OfficialTutorialPractice
{
    public partial class Registration : Form
    {
        //Sql Command objects
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        System.Data.DataTable dt;

        //
        int ID;

        public Registration()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["LocalDBConnectionStringReg"].ConnectionString;
            con = new SqlConnection(connectionString);
            DisplayInfo();
            btnUpdateReg.Enabled = false;
            btnDeleteReg.Enabled = false;
        }
        private void btnSaveReg_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtFName.Text == "" || txtCareer.Text == "" || txtEmail.Text =="" || txtID.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Please Fill in the Blanks");
            }
            else
            {
                try
                {
                    con.Open();
                    string genderReg;

                    if (rbtnMale.Checked)
                    {
                        genderReg = "Male";
                    }
                    else
                    {
                        genderReg = "Female";
                    }

                    cmd = new SqlCommand("InsertEmployee", con);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    };

                    // Parameters to avoid SQL Injection
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@FName", txtFName.Text);
                    cmd.Parameters.AddWithValue("@Career", txtCareer.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@EmpID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Gender", genderReg);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information has been Saved and Sent to the Database!");
                    con.Close();
                    ClearInfo();
                    DisplayInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception Warning");
                }
            }
        }
        // Clears the text boxes once user has sent information to the database!
        public void ClearInfo()
        {
            txtName.Text = "";
            txtFName.Text = "";
            txtCareer.Text = "";
            txtEmail.Text = "";
            txtID.Text = "";
            txtAddress.Text = "";
        }

        //
        public void DisplayInfo()
        {
            try
            {
                dt = new System.Data.DataTable();
                con.Open();

                cmd = new SqlCommand("GetEmployees", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                adapt = new SqlDataAdapter(cmd);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Warning");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCareer.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            //
            rbtnMale.Checked = true;
            rbtnFemale.Checked = false;
            //
            if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "Female")
            {
                rbtnMale.Checked = false;
                rbtnFemale.Checked = true;
            }

            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

            // Enable Buttons again after double click
            btnUpdateReg.Enabled = true;
            btnDeleteReg.Enabled = true;
        }

        private void btnUpdateReg_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string genderReg;
                if (rbtnMale.Checked)
                {
                    genderReg = "Male";
                }
                else
                {
                    genderReg = "Female";
                }

                cmd = new SqlCommand("UpdateEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Parameters to avoid SQL Injection
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@FName", txtFName.Text);
                cmd.Parameters.AddWithValue("@Career", txtCareer.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@EmpID", txtID.Text);
                cmd.Parameters.AddWithValue("@Gender", genderReg);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("The Information has bee Updated in the Database!");
                con.Close();
                ClearInfo();
                DisplayInfo();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Warning");
            }
        }

        private void btnDeleteReg_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                cmd = new SqlCommand("DeleteEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add parameter to avoid SQL Injection
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("The Record has been Deleted");
                con.Close();
                DisplayInfo();
                ClearInfo();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Warning");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the search text from the TextBox
            string searchText = txtSearch.Text;

            // Create DataTable to hold search results
            dt = new System.Data.DataTable();

            try
            {
                con.Open();

                //
                cmd = new SqlCommand("SearchEmployee", con)
                {
                    CommandType= CommandType.StoredProcedure
                };

                //
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                // Execute the query and fill the DataTable
                adapt = new SqlDataAdapter(cmd);
                adapt.Fill(dt);

                con.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Warning");
            }

            // Bind the results to the DataGridView
            dataGridView1.DataSource = dt;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)Excel.ActiveSheet;
                Excel.Visible = true;

                for (int j = 2; j <= dataGridView1.Rows.Count; j++)
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        ws.Cells[j, i] = dataGridView1.Rows[j - 2].Cells[i - 1].Value;
                    }
                }

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    ws.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            catch (Exception) 
            { 
                // Ignored Exception
            }
        }
    }
}
