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
                dataGridView1.DataSource = ds.Tables["Cust"];

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

            f.ShowDialog();

            if(f.lbAddCustomerForm.Text=="Add Customer")
            {
                if (f.txtName.Text == "")
                {
                    mess_alert.info("Customer name is required!", "Required Field");
                    f.txtName.Focus();
                    return;
                }else if (f.txtPhone.Text == "")
                {
                    mess_alert.info("Customer phone is required!", "Required Field");
                    f.txtPhone.Focus();
                    return;
                }else if (f.txtEmail.Text == "")
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

                    }catch(Exception ex)
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
}
