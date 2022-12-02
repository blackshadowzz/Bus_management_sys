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
    public partial class TravelScheduleform : Form
    {
        public TravelScheduleform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        AddTravelScheduleForm f=new AddTravelScheduleForm();

        void addTime()
        {
            string[] times = 
            { "7:00 AM", "7:30 AM",
              "8:00 AM",
              "8:30 AM",
              "9:00 AM",
              "9:30 AM",
              "10:00 AM",
              "11:00 AM",
              "12:00 PM",
              "1:00 PM",
              "1:30 PM",
              "2:30 PM",
              "3:30 PM",
              "4:30 PM",
              "5:30 PM"
            };
            foreach (string t in times) {
                f.cbStartTime.Items.Add(t);
                f.cbArrivelTime.Items.Add(t);
            }
            
            //f.cbStartTime.Items.Add("7:00 AM");
            //f.cbStartTime.Items.Add("7:00 AM");
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
            f.cbBus.DataSource = ds.Tables["bus"];
            f.cbBus.DisplayMember = "bus_number";
            f.cbBus.ValueMember = "id";
            ds.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();

        }
        void viewDriver()
        {
            int status = 0;
            OracleCommand cmd = new OracleCommand("showDriver", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("dstatus", status);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Drive");
            f.cbDriver.DataSource = ds.Tables["Drive"];
            f.cbDriver.DisplayMember = "first_name";
            f.cbDriver.ValueMember= "id";

            ds.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        void clearData()
        {
            f.txtID.Clear();
            f.txtStartPoint.Clear();
            f.txtDestination.Clear();
            f.cbStartTime.Text = "";
            f.cbArrivelTime.Text = "";
            f.rbDescription.Clear();
            f.ndAmountPerSeat.TabIndex= 0;
            
        }
        void viewTravelSchedule()
        {
            try
            {

                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showTravelSchedule", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("tstatus", status);
                OracleDataAdapter ad= new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Travel");
                dataGridView1.DataSource = ds.Tables["Travel"];

                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();

            }catch(Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            f.rbShowMessage.Visible = false;
            f.Text = "Add Travel Schedule";
            f.btnSave.Text = "Save";
            viewDriver();
            viewBus();
            //addTime();
            clearData();
            f.ShowDialog();

            if(f.DialogResult== DialogResult.OK)
            {
                if (f.cbBus.Text == "")
                {
                    mess_alert.info("Please select bus!", "Field Required");
                    f.cbBus.Focus();
                    return;
                }else if (f.cbDriver.Text == "")
                {
                    mess_alert.info("Please select driver!", "Field Required");
                    f.cbDriver.Focus();
                    return;
                }else if (f.txtStartPoint.Text == "")
                {
                    mess_alert.info("Starting point is required!", "Field Required");
                    f.txtStartPoint.Focus();
                    return;
                }else if (f.txtDestination.Text == "")
                {
                    mess_alert.info("Destination to is required!", "Field Required");
                    f.txtDestination.Focus();
                    return;
                }
                else
                {

                }
                if (f.btnSave.Text == "Save")
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("addTravelSchedule", conn);
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.Add("t_bus", Convert.ToInt32(f.cbBus.SelectedValue));
                        cmd.Parameters.Add("t_driver",Convert.ToInt32(f.cbDriver.SelectedValue));
                        cmd.Parameters.Add("t_start_point", f.txtStartPoint.Text);
                        cmd.Parameters.Add("t_destination",f.txtDestination.Text);
                        cmd.Parameters.Add("t_date",OracleDbType.Date).Value=f.dtpScheduleDate.Value;
                        cmd.Parameters.Add("t_start_time", f.cbStartTime.Text);
                        cmd.Parameters.Add("t_arrival_time",f.cbArrivelTime.Text);
                        cmd.Parameters.Add("t_amount",decimal.Parse(f.ndAmountPerSeat.Text));
                        cmd.Parameters.Add("t_desc",f.rbDescription.Text);
                        cmd.Parameters.Add("t_by", "Mango");
                        cmd.ExecuteNonQuery();

                        clearData();
                        viewTravelSchedule();
                        mess_alert.info("One record has been added successfully!", "Add Travel Schedule");



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
            
        }

        private void TravelScheduleform_Load(object sender, EventArgs e)
        {
            viewTravelSchedule();
            btnDelete.Enabled= false;
            btnEdit.Enabled= false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewTravelSchedule();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            //create command text
          
            OracleCommand cmd = new OracleCommand("searchTravelSchedule", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //add value for parameter stor procedure
            cmd.Parameters.Add("start_point", txtSearch.Text.Trim());
            

            //create dataAdapter object
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = cmd;
            //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds=new DataSet();
            adapter.Fill(ds,"Travel");
            dataGridView1.DataSource = ds.Tables["Travel"];

            ds.Dispose();
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            f.Text = "Update Travel Schedule";
            f.btnSave.Text = "Update Now";
            f.rbShowMessage.Visible = false;
            viewDriver();
            viewBus();
            addTime();

            f.txtID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            f.cbBus.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            f.cbDriver.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f.txtStartPoint.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            f.txtDestination.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            f.dtpScheduleDate.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            f.cbStartTime.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            f.cbArrivelTime.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            f.ndAmountPerSeat.Value =Convert.ToDecimal( dataGridView1.CurrentRow.Cells[9].Value.ToString());
            f.rbDescription.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();

            if (f.txtID.Text == "")
            {
                mess_alert.warning("Please select any record to update!", "Update Travel Schedule");
                return;
            }
            f.ShowDialog();
            
            if (f.DialogResult== DialogResult.OK)
            {
                if(f.btnSave.Text=="Update Now")
                {
                    if (MessageBox.Show("Do you want to update this record?", "Update Record",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                }
                else
                {

                }
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
}
