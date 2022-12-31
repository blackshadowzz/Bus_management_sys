using Oracle.ManagedDataAccess.Client;
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
using System.Diagnostics;

namespace BusMS
{
    public partial class Paymentform : Form
    {
        public Paymentform()
        {
            InitializeComponent();
        }

        OracleConnection conn = DBConnection.Connection();
        //void viewBooking()
        //{
        //    try
        //    {

        //        int status = 0;
        //        OracleCommand cmd
        //            = new OracleCommand("showBooking", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("bstatus", status);
        //        OracleDataAdapter ad = new OracleDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        ad.Fill(ds, "book");
        //        cbBooking.DataSource= ds.Tables["book"];
        //        cbBooking.DisplayMember= "id";
        //        cbBooking.ValueMember = "id";
        //        ds.Dispose();
        //        ad.Dispose();
        //        cmd.Dispose();


        //    }
        //    catch (Exception ex)
        //    {
        //        mess_alert.error(ex.Message, "Error");
        //    }
        //    finally { conn.Close(); }
        //}
        void viewBooking()
        {
            try
            {
                conn.Open();
                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showBooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("tstatus", status);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //reader["Start Point"] + " - " +
                    cbBooking.Items.Add(reader["id"]);
                    cbBooking.DisplayMember = reader["id"].ToString();
                    cbBooking.ValueMember = reader["id"].ToString();
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
        private void Paymentform_Load(object sender, EventArgs e)
        {
            viewBooking();
            viewPayment();
        }
        int schedule_id;
        double price,total;

        private void btnPayNow_Click(object sender, EventArgs e)
        {
            double cash, sum=0,change,pay;
                cash= double.Parse(txtCash.Text);
            if (total > cash)
            {
                sum = total - cash;
                change = sum;
                lbChangeDollar.Text = change.ToString("+ #,##0.00 $");
                change = sum * 4100;
                lbChangeReil.Text = change.ToString("+ #,##0.00 $");
            }
            else
            {
                sum = total-cash;
                change = sum;
                lbChangeDollar.Text = change.ToString("#,##0.00 $");
                change = sum * 4100;
                lbChangeReil.Text = change.ToString("#,##0.00 $");
            }
            lbTotalDollar.Text = total.ToString("#,##0.00 $");
            lbTotalReil.Text = (total * 4100).ToString("#,##0.00 $");
            lbPayDollar.Text = total.ToString("#,##0.00 $");
            lbPayReil.Text = (total * 4100).ToString("#,##0.00 $");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("addPayment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pbookingID", schedule_id);
                cmd.Parameters.Add("pamount", total);
                cmd.Parameters.Add("pby", userLoginClass.getUsername());
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                viewPayment();
            }catch(Exception ex)
            {
                mess_alert.error(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void viewPayment()
        {
            try
            {

                int status = 0;
                OracleCommand cmd
                    = new OracleCommand("showPayment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("bstatus", status);
                OracleDataAdapter ad = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "pay");
                dataGridView1.DataSource = ds.Tables["pay"];
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
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

        private void cbBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand("showbookingPay", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pid", cbBooking.SelectedItem);
            //cmd.Parameters.Add("bstart", cbSchedule.SelectedItem);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                schedule_id = int.Parse(reader[0] + "");
                price = double.Parse(reader[1].ToString());
                total = double.Parse(reader[2].ToString());
                txtPerSeatPrice.ForeColor = Color.Red;
                txtTotalPrice.ForeColor = Color.Red;
                txtTotalPrice.Text = total.ToString("#,##0.00 $");
                txtPerSeatPrice.Text = price.ToString("#,##0.00 $");
                lbDiscount.Text = reader[3].ToString();


                //schedule_id = int.Parse(reader["id"].ToString());
            }
            conn.Close();
        }
    }
}
