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
    public partial class Bookingform : Form
    {
        public Bookingform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        OracleCommand cmd;

        void viewSchedule()
        {
            try
            {
                conn.Open();
                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showTravelSchedule", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("tstatus", status);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //reader["Start Point"] + " - " +
                    cbSchedule.Items.Add(reader["destination"]);
                    cbSchedule.DisplayMember = reader["destination"].ToString();
                    cbSchedule.ValueMember = reader["id"].ToString();
                }
                //OracleDataAdapter ad = new OracleDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //ad.Fill(ds, "Travel");
                //cbSchedule.DataSource= ds.Tables["Travel"];
                //cbSchedule.DisplayMember = "Destination";
                //cbSchedule.ValueMember= "id";

                //ds.Dispose();
                //ad.Dispose();
                //cmd.Dispose();
                reader.Dispose();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }
        void viewCustomer()
        {
            try
            {

                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("c_status", status);
                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds,"Cust");
                cbCustomer.DataSource = ds.Tables["Cust"];
                cbCustomer.DisplayMember = "name";
                cbCustomer.ValueMember = "id";

                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();
               

            }
            catch (Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }
        void viewBooking()
        {
            try
            {

                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showBooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("bstatus", status);
                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "book");
                dataGridView1.DataSource = ds.Tables["book"];
                dataGridView1.Columns[0].Width = 35;
                dataGridView1.Columns[5].Width = 45;
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[10].Width = 60;
                int count=dataGridView1.RowCount;
                lbBookingTotal.Text = count.ToString();
                dataGridView1.Columns[11].Visible = false;
                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }
        private void Bookingform_Load(object sender, EventArgs e)
        {
            txtID.Visible= false;
            cbSchedule.Text = "";
            cbBookingStatus.Items.Add("Paid");
            cbBookingStatus.Items.Add("Pedding");
            viewSchedule();
            viewCustomer();
            btnCalculate.Enabled = false;
            btnCancelOrDelete.Enabled = false;
            btnBooking.Enabled = false;
            btnDelete.Visible=false;
            viewBooking();
            totalIncome();
            
            
        }
        void totalIncome()
        {
            double totalIncome = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            { 
                double total =Double.Parse(dataGridView1.Rows[i].Cells[8].Value+"");
                totalIncome+= total;
            }
            lbTotalIncome.ForeColor = Color.Red;
            lbTotalIncome.Text = totalIncome.ToString("#,##0.00 $");
        }
        int schedule_id, noOfSeat;
        double price, total,discount,subTotal;

        private void btnBooking_Click(object sender, EventArgs e)
        {
            if (lbTotalAmount.Text == "")
            {
                mess_alert.info("Please calculate total amount first", "Booking");
                return;
            }else if (cbWhereSeat.Text == "")
            {
                mess_alert.info("Please enter where seat!", "Booking");
                return;
            }else if (cbBookingStatus.Text == "")
            {
                mess_alert.info("Booking status is required", "Booking");
                return;
            }
            if (btnBooking.Text == "Booking")
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("addBooking",conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Add("bsched_id", schedule_id);
                    cmd.Parameters.Add("bcust_id", cbCustomer.SelectedValue);
                    cmd.Parameters.Add("bno_seat",numericUpDownNoOfSeat.Value);
                    cmd.Parameters.Add("bamount_seat", decimal.Parse(lbPricePerSeat.Text));
                    cmd.Parameters.Add("bdiscount",double.Parse(txtDiscount.Text));
                    cmd.Parameters.Add("btotal_amount", decimal.Parse(lbTotalAmount.Text));
                    cmd.Parameters.Add("bwhere_seat", cbWhereSeat.Text);
                    cmd.Parameters.Add("bbook_status", cbBookingStatus.Text);
                    cmd.Parameters.Add("bby", "Mango");
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    viewBooking();
                    mess_alert.info("One record has been added successfully", "Booking");
                    clearData();
                    totalIncome();
                }catch(Exception ex)
                {
                    mess_alert.error(ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
            }
            if (btnBooking.Text == "Update")
            {
                if(MessageBox.Show("Do you want to update this record?","Update Booking",MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd= new OracleCommand("updateBooking",conn);
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.Add("bid",int.Parse(txtID.Text));
                        cmd.Parameters.Add("bsched_id", schedule_id);
                        cmd.Parameters.Add("bcust_id", cbCustomer.SelectedValue);
                        cmd.Parameters.Add("bno_seat", numericUpDownNoOfSeat.Value);
                        cmd.Parameters.Add("bamount_seat", decimal.Parse(lbPricePerSeat.Text));
                        cmd.Parameters.Add("bdiscount", decimal.Parse(txtDiscount.Text));
                        cmd.Parameters.Add("btotal_amount",decimal.Parse(lbTotalAmount.Text));
                        cmd.Parameters.Add("bwhere_seat",cbWhereSeat.Text);
                        cmd.Parameters.Add("bbook_status", cbBookingStatus.Text);
                        cmd.Parameters.Add("bby", "Admin");
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        viewBooking();
                        mess_alert.info("One record has been updated successfully!", "Update Booking");
                        clearData();

                    }catch(Exception ex ) {
                        mess_alert.error(ex.Message, "Error");
                    }
                    finally { conn.Close(); }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearData();
            viewBooking();
            //Bookingform_Load(null, null);
            totalIncome();
        }

        private void btnCancelOrDelete_Click(object sender, EventArgs e)
        {
            clearData();
            btnBooking.Enabled= false;
            btnCalculate.Enabled= false;
            btnBooking.Text = "Booking";
            btnDelete.Visible= false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Visible = true;
            btnBooking.Enabled = true;
            btnCancelOrDelete.Enabled=true;
            btnBooking.Text = "Update";
            btnCancelOrDelete.Text= "Clear";

            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cbCustomer.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            lbFrom.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            lbDest.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cbSchedule.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            lbPricePerSeat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            numericUpDownNoOfSeat.Value =Convert.ToInt32( dataGridView1.CurrentRow.Cells[5].Value.ToString());
            cbWhereSeat.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtDiscount.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            lbTotalAmount.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            cbBookingStatus.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            //conn.Open();
            //int status = 0;
            //OracleCommand cmd = new OracleCommand("searchBooking", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("bstatus", status);
            //cmd.Parameters.Add("bid",txtSearch.Text.Trim());

            //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
           
            //DataSet dataSet= new DataSet();
            //adapter.Fill(dataSet,"Book");
            //dataGridView1.DataSource= dataSet.Tables["Book"];
            //dataSet.Dispose();
            //adapter.Dispose();
            //cmd.Dispose();
            //conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                mess_alert.warning("Please select any record to delete!", "Delete Booking");
                return;
            }
            if(MessageBox.Show("Do you want to delete this record?","Delete Booking",MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    int status = 1;
                    OracleCommand cmd= new OracleCommand("deleteBooking",conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Add("bid", int.Parse(txtID.Text));
                    cmd.Parameters.Add("bstatus", status);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    viewBooking();
                    mess_alert.info("One record has been deleted successfully!", "Delete Booking");
                    clearData();
                    totalIncome();

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

        private void btnPedding_Click(object sender, EventArgs e)
        {
            try
            {

              
                OracleCommand cmd
                    = new OracleCommand("showBookingPedding", conn);
                cmd.CommandType = CommandType.StoredProcedure;
               
                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "book");
                dataGridView1.DataSource = ds.Tables["book"];
                dataGridView1.Columns[0].Width = 35;
                dataGridView1.Columns[5].Width = 45;
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[10].Width = 60;
                int count = dataGridView1.RowCount;
                lbBookingTotal.Text = count.ToString();
                dataGridView1.Columns[11].Visible = false;
                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            try
            {


                OracleCommand cmd
                    = new OracleCommand("showBookingPaid", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "book");
                dataGridView1.DataSource = ds.Tables["book"];
                dataGridView1.Columns[0].Width = 35;
                dataGridView1.Columns[5].Width = 45;
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[10].Width = 60;
                int count = dataGridView1.RowCount;
                lbBookingTotal.Text = count.ToString();
                dataGridView1.Columns[11].Visible = false;
                ds.Dispose();
                ad.Dispose();
                cmd.Dispose();


            }
            catch (Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally { conn.Close(); }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            viewBooking();
        }

        private void clearData()
        {
            cbSchedule.Text = "";
            cbCustomer.Text = string.Empty;
            lbDest.Text = "";
            lbFrom.Text = "";
            txtDiscount.Clear();
            lbPricePerSeat.Text = "";
            lbTimeStartEnd.Text = "";
            cbBookingStatus.Text = "";
            numericUpDownNoOfSeat.Value = 0;
            
            lbTotalAmount.Text = "";
            cbWhereSeat.Items.Clear();
            noOfSeat = 0;
            cbWhereSeat.Text = "";
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            
            if (lbPricePerSeat.Text == "")
            {
                mess_alert.info("Please select destination!", "Booking");
                return;
            }else if (numericUpDownNoOfSeat.Value == 0)
            {
                mess_alert.info("Number of Seat is required!", "Booking");
                return;
            }
            price = double.Parse(lbPricePerSeat.Text);
            noOfSeat = Convert.ToInt32(numericUpDownNoOfSeat.Value);
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
                discount = double.Parse(txtDiscount.Text);
            }
            else
            {
                discount = double.Parse(txtDiscount.Text);
            }
            
            subTotal = price * noOfSeat;
            total = subTotal - subTotal * (discount / 100);
            lbTotalAmount.ForeColor = Color.Red;
            lbTotalAmount.Text = total.ToString();


        }

        private void cbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            cbWhereSeat.Items.Clear();

            btnCancelOrDelete.Text = "Cancel Booking";
            btnDelete.Visible=false;
            btnCalculate.Enabled = true;
            btnBooking.Enabled = true;
            btnCancelOrDelete.Enabled = true;
            //int status = 0;
            OracleCommand cmd = new OracleCommand("showScheduleBooking", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("bdest", cbSchedule.SelectedItem);
            //cmd.Parameters.Add("bstart", cbSchedule.SelectedItem);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                schedule_id =int.Parse( reader[0] + "");
                price = double.Parse(reader[3].ToString());
         
                lbDest.ForeColor = Color.Red;
                lbFrom.ForeColor = Color.OrangeRed;
                lbPricePerSeat.ForeColor = Color.Red;
                lbTimeStartEnd.ForeColor = Color.LightSeaGreen;
                lbFrom.Text = reader[2] + "";
                lbDest.Text= reader[1] + "";
                lbTimeStartEnd.Text = reader[4] + "" + " - " + reader[5] + "";
                txtBusType.Text = reader[7] + "";
                txtCapSeat.Text = reader[6] + "";
                
                lbPricePerSeat.Text = price.ToString();
                //schedule_id = int.Parse(reader["id"].ToString());
            }

            int noSeat=int.Parse(txtCapSeat.Text);
            string whereSeat = "";
            for(int i = 1; i <= noSeat; i++)
            {
                
                if (i > 9)
                {
                    whereSeat = "No " + i;
                    cbWhereSeat.Items.Add(whereSeat);
                }
                else
                {
                    whereSeat = "No 0" + i;
                    cbWhereSeat.Items.Add(whereSeat);
                }
                
                
            }
            noOfSeat = 0;
           
            //cbWhereSeat.Items.Add(whereSeat);

            conn.Close();
        }
    }
}
