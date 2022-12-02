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
    public partial class Busform : Form
    {
        public Busform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        private void Busform_Load(object sender, EventArgs e)
        {
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            viewBus();
            cbBusType.Items.Add("Bus");
            cbBusType.Items.Add("Ssamsung");
            cbBusType.Items.Add("Pick Up");
            cbBusType.Items.Add("Big Bus");
        }

        string id;

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (btnAddNew.Text == "Clear")
            {
                clearData();
                btnAddNew.Text = "Add New";
                btnDelete.Text = "Cancel";
                btnUpdate.Enabled = false;
            }
            else if(btnAddNew.Text=="Add New")
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd= new OracleCommand("addBus",conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Add("btype", cbBusType.Text);
                    cmd.Parameters.Add("bnumber",txtBusNumber.Text);
                    cmd.Parameters.Add("bplateno",txtBusPlateNo.Text);
                    cmd.Parameters.Add("bcapseat", txtBusCapSeat.Text);
                    cmd.Parameters.Add("bby", "Mango");
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    viewBus();
                    mess_alert.info("One record has been added successfully!", "Add Bus");
                    clearData();

                }catch(Exception ex)
                {
                    mess_alert.error(ex.Message,"Error");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void clearData()
        {
            txtBusCapSeat.Clear();
            txtBusNumber.Clear();
            txtBusPlateNo.Clear();
            txtSearch.Clear();
            cbBusType.SelectedItem=null;
        }

        private void viewBus()
        {
            try
            {
                int status = 0;
                OracleCommand cmd = new OracleCommand("showBus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("bstatus", status);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];

            }
            catch(Exception ex)
            {
                mess_alert.error(ex.Message,"Error");

            }
            finally { conn.Close(); }
            


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            int status = 0;
            //create command text
            OracleCommand cmd = new OracleCommand("searchBus", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //add value for parameter stor procedure
            cmd.Parameters.Add("bustype", txtSearch.Text.Trim());
            cmd.Parameters.Add("bstatus", status);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAddNew.Text = "Clear";
            btnDelete.Text = "Delete";
            btnDelete.Enabled = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Enabled = true;

            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cbBusType.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtBusNumber.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtBusPlateNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtBusCapSeat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                if (btnDelete.Text == "Cancel")
                {
                    clearData();
                    btnDelete.Text = "Delete";
                    btnDelete.Enabled = false;
                    btnAddNew.Text = "Add New";
                    btnUpdate.Text = "Update";
                }
                else if (btnDelete.Text == "Delete")
                {
                    if (MessageBox.Show("Do you want to delete this record!", "Delete Bus",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(txtBusNumber.Text))
                        {
                            mess_alert.warning("Please select record to delete!", "Delete Bus");

                        }
                        else
                        {
                            try
                            {
                                conn.Open();
                                OracleCommand cmd = new OracleCommand("deleteBus", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("b_id", OracleDbType.Int32).Value = int.Parse(id);
                                cmd.Parameters.Add("b_status", 1);
                                cmd.ExecuteNonQuery();

                                cmd.Dispose();
                                viewBus();
                                mess_alert.warning("One record has been removed successfully!", "Delete Employee");
                                clearData();
                                btnAddNew.Text = "Add New";
                                btnUpdate.Enabled = false;

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to update this record!", "Update Bus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (btnUpdate.Text == "Update")
                {
                    if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(txtBusNumber.Text))
                    {
                        mess_alert.warning("Please select any record to update!", "Update Bus");
                        return;
                    }
                    else
                    {
                        try
                        {
                            conn.Open();
                            OracleCommand cmd = new OracleCommand("updateBus", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("b_id", int.Parse(id));
                            cmd.Parameters.Add("b_type", cbBusType.Text);
                            cmd.Parameters.Add("b_number", txtBusNumber.Text);
                            cmd.Parameters.Add("b_plat_no", txtBusPlateNo.Text);
                            cmd.Parameters.Add("b_cap_seat", txtBusCapSeat.Text);
                            cmd.Parameters.Add("b_by", "Mango");
                            cmd.ExecuteNonQuery();

                            cmd.Dispose();
                            viewBus();
                            mess_alert.info("One record has been update successfully!", "Update Bus");
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
}
