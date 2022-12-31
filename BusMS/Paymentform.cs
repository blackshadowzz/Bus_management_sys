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
                cbBooking.DataSource= ds.Tables["book"];
                cbBooking.DisplayMember= "id";
                cbBooking.ValueMember = "id";
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
        private void Paymentform_Load(object sender, EventArgs e)
        {
            viewBooking();
        }
        int schedule_id;
        double price;
        private void cbBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("showScheduleBooking", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("bdest", cbBooking.SelectedValue);
            //cmd.Parameters.Add("bstart", cbSchedule.SelectedItem);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                schedule_id = int.Parse(reader[0] + "");
                price = double.Parse(reader[4].ToString());

               
                //schedule_id = int.Parse(reader["id"].ToString());
            }
        }
    }
}
