using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace BusMS
{
    public partial class Employeeform : Form
    {
        public Employeeform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        int id;
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (btnAddNew.Text == "Add New")
            {
                btnAddNew.Text = "Save";
                btnUpdate.Enabled= false;
                btnDelete.Text = "Cancel";
                txtFname.Focus();
                clearData();
            }
            else if (btnAddNew.Text == "Save")
            {
                if (string.IsNullOrEmpty(txtFname.Text))
                {
                    MessageBox.Show("Please enter category name!");
                    txtFname.Focus();
                    return;
                }
                else
                {
                    try
                    {
                        conn.Open();
                        //Create command object
                        OracleCommand cmdInsert = new OracleCommand("addEmp", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;
                        // add values for parameter of Store procedure
                        cmdInsert.Parameters.Add("e_fname",txtFname.Text);
                        cmdInsert.Parameters.Add("e_lname", txtLname.Text);
                        cmdInsert.Parameters.Add("e_dob", dtpDob.Value);
                        cmdInsert.Parameters.Add("e_gender", cbGender.Text);
                        cmdInsert.Parameters.Add("e_phone", txtPhone.Text);
                        cmdInsert.Parameters.Add("e_email", txtEmail.Text);
                        cmdInsert.Parameters.Add("e_address", txtAddress.Text);
                        cmdInsert.Parameters.Add("e_role", cbRole.Text);
                        cmdInsert.Parameters.Add("e_salary", decimal.Parse(txtSalary.Text));
                        cmdInsert.Parameters.Add("e_by", "Mango");
                        cmdInsert.ExecuteNonQuery();

                        cmdInsert.Dispose();
                        showData();
                        mess_alert.info("One record has been created successfully!", "Create Employee");
                        clearData();

                        btnAddNew.Text = "Add New";
                        btnUpdate.Enabled= true;
                        btnDelete.Text="Remove";


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        void clearData()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtPhone.Clear();
            txtSalary.Clear();
            txtSearch.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            cbGender.Text= string.Empty;
            cbRole.Text= string.Empty;
            dtpDob.Text= string.Empty;
        }
        void showData()
        {
            OracleCommand cmd = new OracleCommand("showEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter ad = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, "emp");
            dataGridView1.DataSource = ds.Tables["emp"];
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 150;

            cmd.Dispose();
            ad.Dispose();
            ds.Dispose();
            conn.Close();
        }

        private void Employeeform_Load(object sender, EventArgs e)
        {
            showData();

            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.Items.Add("Others");


            cbRole.Items.Add("Employee");
            cbRole.Items.Add("Driver");
        }
    }
}
