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
    public partial class Driverform : Form
    {
        public Driverform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        AddDriverForm form = new AddDriverForm();
        void viewEmp()
        {
            OracleCommand cmd = new OracleCommand("showEmp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "emp");
            form.cbEmployee.DataSource = ds.Tables["emp"];
            form.cbEmployee.DisplayMember = "first_name";
            form.cbEmployee.ValueMember = "id";
            ds.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();

        }
        void viewBus()
        {

            int status = 0;
            OracleCommand cmd = new OracleCommand("showBus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("bstatus", status);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "bus");
            form.cbBus.DataSource = ds.Tables["bus"];
            form.cbBus.DisplayMember = "bus_number";
            form.cbBus.ValueMember = "id";
            ds.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();

        }

        void viewDriver()
        {
            //try
            //{
                int status = 0;
                OracleCommand cmd = new OracleCommand("showDriver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("dstatus", status);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Drive");
                dataGridView1.DataSource = ds.Tables["Drive"];

                ds.Dispose();
                adapter.Dispose();
                cmd.Dispose();
                conn.Close();
            //}
            //catch(Exception ex)
            //{
            //    mess_alert.error(ex.Message,"Error");
            //}
            
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            viewBus();
            viewEmp();
            form.txtDriverID.Clear();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {


                if (form.btnSave.Text == "Save")
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("addDriver",conn);
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.Add("demp",Convert.ToInt32(form.cbEmployee.SelectedValue.ToString()));
                        cmd.Parameters.Add("dbus",Convert.ToInt32(form.cbBus.SelectedValue.ToString()));
                        cmd.Parameters.Add("d_by", "Mango");
                        cmd.ExecuteNonQuery();

                        viewDriver();
                        mess_alert.info("One record has been created successfully!", "Add Driver");

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

        }

        private void Driverform_Load(object sender, EventArgs e)
        {
            viewDriver();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            viewBus();
            viewEmp();
            //if (form.txtDriverID.Text == "")
            //{
            //    mess_alert.warning("Please select any to update!", "Update Driver");
            //}
            form.txtDriverID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            form.cbEmployee.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            form.cbBus.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            form.ShowDialog();

            if(form.DialogResult== DialogResult.OK)
            {
               if(MessageBox.Show("Do you want to update driver record?","Update Driver"
                   ,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("updateDriver",conn);
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.Add("d_id", Convert.ToInt32(form.txtDriverID.Text));
                        cmd.Parameters.Add("d_emp", Convert.ToInt32(form.cbEmployee.SelectedValue));
                        cmd.Parameters.Add("d_bus",Convert.ToInt32(form.cbBus.SelectedValue));
                        cmd.Parameters.Add("d_by", "Mango");
                        cmd.ExecuteNonQuery();
                         cmd.Dispose();
                        viewDriver();
                        mess_alert.info("One rcord has been update successfully!", "Update Driver");
                        form.txtDriverID.Clear();

                    }catch(Exception ex)
                    {
                        mess_alert.error(ex.Message, "Error");
                    }
                }
            }
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete driver record?", "Delete Driver"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    int status = 1;
                    OracleCommand cmd = new OracleCommand("deleteDriver", conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Add("d_id", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    cmd.Parameters.Add("d_status", status);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    viewDriver();
                    mess_alert.info("One record has been deleted successfully!", "Delete Driver");


                }catch(Exception ex)
                {
                    mess_alert.error(ex.Message, "Error");
                }
                finally { conn.Close(); }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewDriver();
        }
    }
}
