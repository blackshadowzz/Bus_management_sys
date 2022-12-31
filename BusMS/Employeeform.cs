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
        string id;
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
            dtpDob.Text = "";
        }
        void showData()
        {
            OracleCommand cmd = new OracleCommand("showEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter ad = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, "emp");
            dataGridView1.DataSource = ds.Tables["emp"];
            int count = dataGridView1.RowCount;
            lbEmpTotal.Text = count.ToString();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[11].Visible = false;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(btnUpdate.Text=="Update")
            {
                if(string.IsNullOrEmpty(id)&& string.IsNullOrEmpty(txtFname.Text))
                {
                    mess_alert.warning("Please select record to update!", "Update Employee");

                }
                else
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("updateEmp", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("e_id", OracleDbType.Int32).Value = int.Parse(id);
                        cmd.Parameters.Add("e_fname", txtFname.Text);
                        cmd.Parameters.Add("e_lname", txtLname.Text);
                        cmd.Parameters.Add("e_dob", dtpDob.Value);
                        cmd.Parameters.Add("e_gender", cbGender.Text);
                        cmd.Parameters.Add("e_phone", txtPhone.Text);
                        cmd.Parameters.Add("e_email", txtEmail.Text);
                        cmd.Parameters.Add("e_address", txtAddress.Text);
                        cmd.Parameters.Add("e_role", cbRole.Text);
                        cmd.Parameters.Add("e_salary", decimal.Parse(txtSalary.Text));
                        cmd.Parameters.Add("e_upby", "Mango");
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        showData();
                        mess_alert.info("One record has been updated successfully!", "Update Employee");
                        clearData();


                    }
                    catch(Exception ex)
                    {
                        mess_alert.error(ex.Message, "Error");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id =dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtpDob.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cbGender.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            cbRole.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "Cancel")
            {
                clearData();
                btnDelete.Text = "Remove";
                btnUpdate.Enabled = true;
                btnAddNew.Text = "Add New";
            }else if (btnDelete.Text == "Remove")
            {
                if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(txtFname.Text))
                {
                    mess_alert.warning("Please select record to remove!", "Remove Employee");

                }
                else
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("deleteEmp", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("e_id", OracleDbType.Int32).Value = int.Parse(id);
                        cmd.Parameters.Add("e_status", 1);
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        showData();
                        mess_alert.warning("One record has been removed successfully!", "Delete Employee");
                        clearData();


                    }
                    catch (Exception ex)
                    {
                        mess_alert.error(ex.Message, "Error");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            //create command text
            OracleCommand cmd = new OracleCommand("searchEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //add value for parameter stor procedure
            cmd.Parameters.Add("e_fname", txtSearch.Text.Trim()); 

            //create dataAdapter object
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = cmd;
            //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            dt.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();
        }
    }
}
