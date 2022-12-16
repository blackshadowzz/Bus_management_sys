using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace BusMS
{
    public partial class Customerform : Form
    {
        public Customerform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        readonly AddCustomerForm f2 = new AddCustomerForm();
        void viewCustomer()
        {
            //try
            //{
                int status = 0;
                OracleCommand cmd = new OracleCommand("showCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("c_status", status);
                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cust");
                dataGridView1.RowTemplate.Height = 35;

                dataGridView1.DataSource = ds.Tables["Cust"];
                dataGridView1.Columns[9].Visible = false;

                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();
                conn.Close();

            //}catch(Exception ex)
            //{
            //    mess_alert.error(ex.Message, "Error");
            //}
            //finally
            //{
            //    conn.Open();
            //}
        }
        void clearData()
        {
            f2.txtID.Clear();
            f2.txtName.Clear();
            f2.txtPhone.Clear();
            f2.txtEmail.Clear();
            f2.rbAddress.Clear();
            f2.rbMessages.Clear();
        }
        private void Customerform_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            viewCustomer();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddCustomerForm f = new AddCustomerForm();
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            f.ShowDialog();

            if(f.lbAddCustomerForm.Text=="Add Customer")
            {
                if (f.DialogResult == DialogResult.OK)
                {
                    if (f.txtName.Text == "")
                    {
                        mess_alert.info("Customer name is required!", "Required Field");
                        f.txtName.Focus();
                        return;
                    }
                    else if (f.txtPhone.Text == "")
                    {
                        mess_alert.info("Customer phone is required!", "Required Field");
                        f.txtPhone.Focus();
                        return;
                    }
                    else if (f.txtEmail.Text == "")
                    {
                        mess_alert.info("Customer email is required!", "Required Field");
                        f.txtEmail.Focus();
                        return;
                    }

                    if (f.btnSave.Text == "Save")
                    {
                        try
                        {
                            conn.Open();
                            OracleCommand cmd = new OracleCommand("addCustomer", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("c_name", f.txtName.Text);
                            cmd.Parameters.Add("c_phone", f.txtPhone.Text);
                            cmd.Parameters.Add("c_email", f.txtEmail.Text);
                            cmd.Parameters.Add("c_address", f.rbAddress.Text);
                            cmd.Parameters.Add("c_by", "Admin");
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            viewCustomer();
                            mess_alert.info("One record has been added successfully!", "Add Customer");
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
                else
                {

                }
            }
            
                

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(btnDelete.Text== "Delete")
            {
                if(MessageBox.Show("Do you to delete this record","Delete Customer",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        int status = 1;
                        OracleCommand cmd = new OracleCommand("deleteCustomer", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("c_id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                        cmd.Parameters.Add("c_status", status);
                        cmd.ExecuteNonQuery();
                         cmd.Dispose();
                         viewCustomer();
                        mess_alert.info("One record has been delete successfully!", "Delete Customer");

                    }catch(Exception ex)
                    {
                        mess_alert.error(ex.Message, "Error");
                    }
                    finally { conn.Close(); }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            viewCustomer();
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddCustomerForm edit=new AddCustomerForm();
            edit.lbAddCustomerForm.Text = "Update Customer";
            edit.btnSave.Text = "Update Now";

            edit.txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            edit.txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            edit.txtPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            edit.txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            edit.rbAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            edit.ShowDialog();
            if (edit.DialogResult == DialogResult.OK)
            {
                if (edit.txtName.Text == "")
                {
                    mess_alert.info("Customer name is required!", "Required Field");
                    edit.txtName.Focus();
                    return;
                }
                else if (edit.txtPhone.Text == "")
                {
                    mess_alert.info("Customer phone is required!", "Required Field");
                    edit.txtPhone.Focus();
                    return;
                }
                else if (edit.txtEmail.Text == "")
                {
                    mess_alert.info("Customer email is required!", "Required Field");
                    edit.txtEmail.Focus();
                    return;
                }

                if (edit.btnSave.Text == "Update Now")
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("updateCustomer", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("c_id",int.Parse(edit.txtID.Text));
                        cmd.Parameters.Add("c_name", edit.txtName.Text);
                        cmd.Parameters.Add("c_phone", edit.txtPhone.Text);
                        cmd.Parameters.Add("c_email", edit.txtEmail.Text);
                        cmd.Parameters.Add("c_address", edit.rbAddress.Text);
                        cmd.Parameters.Add("c_by", "Admin");
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();

                        viewCustomer();
                        mess_alert.info("One record has been delete successfully!", "Add Customer");
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
    }
}
