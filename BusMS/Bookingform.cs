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
                    cbSchedule.Items.Add(reader["Start Point"] + " - " + reader["destination"]);
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
        private void Bookingform_Load(object sender, EventArgs e)
        {
            cbSchedule.Text = "";
            viewSchedule();
        }
        int schedule_id;
        double price, total;
        private void cbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();


            //int status = 0;
            OracleCommand cmd = new OracleCommand("showScheduleBooking", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("bdest", cbSchedule.SelectedItem);
            cmd.Parameters.Add("bstart", cbSchedule.SelectedItem);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtID.Text = reader[0] + "";
                price = double.Parse(reader[2].ToString());
                txtPricePerSeat.Text = price.ToString();
                //schedule_id = int.Parse(reader["id"].ToString());
            }

            conn.Close();
        }
    }
}
