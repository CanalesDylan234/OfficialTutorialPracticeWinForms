using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficialTutorialPractice
{
    public partial class Form1 : Form
    {
        // Create DataTable Object
        DataTable dt = new DataTable();
        string name;
        string designation;
        int salary;
        string gender;
        string review;

        // Tutorial 2 variables initialization
        string courseName;

        // Tutorial 3 BIG ONE
        Registration rg = new Registration();
        public Form1()
        {
            InitializeComponent();
            btnReports.Image = Properties.Resources.reportImg;
            btnReg.Image = Properties.Resources.profileReg;
            btnInvoice.Image = Properties.Resources.invoice;
            btnItems.Image = Properties.Resources.add_to_cart;
            btnShop.Image = Properties.Resources.shopImg;
            btnProducts.Image = Properties.Resources.productImage;
            picHome.Image = Properties.Resources._5559852;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            name = txtName.Text;
            designation = txtDesignation.Text;
            salary = int.Parse(txtSalary.Text);

            if (rbtnMale.Checked) 
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }

            if (chkGood.Checked) 
            {
                review = "Good";
            }
            else
            {
                review = "Very Good";
            }

            lblDisplay.Text = name + " " + designation+ " " + salary + " " + gender + " " + review;

            display();
            empty();
        }

        public void display()
        {
            dt.Columns.Add("Name: ");
            dt.Columns.Add("Designation: ");
            dt.Columns.Add("Salary: ");
            dt.Columns.Add("Gender: ");
            dt.Columns.Add("Review: ");

            // Create DataRow Object
            DataRow dr = dt.NewRow();

            dr[0] = name;
            dr[1] = designation;
            dr[2] = salary;
            dr[3] = gender;
            dr[4] = review;

            dt.Rows.Add(dr);
            dtDataGridView.DataSource = dt;

        }

        public void empty()
        {
            txtDesignation.Text = "";
            txtName.Text = "";
            txtSalary.Text = "";
        }

        // Tutotial 2
        private void Form1_Load(object sender, EventArgs e)
        {
            cbCourse.Items.Add("C# Programming");
            cbCourse.Items.Add("C++ Programming");
            cbCourse.Items.Add("C Programming");
            cbCourse.Items.Add("Java Programming");
            cbCourse.Items.Add("PHP Programming");
            cbCourse.Items.Add("Python Programming");
        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            courseName = cbCourse.SelectedItem.ToString();

        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            MessageBox.Show(courseName);
        }

        // Tutorial 3 functions/events
        private void btnReports_Click(object sender, EventArgs e)
        {
    
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            rg.Show();
        }
    }
}
