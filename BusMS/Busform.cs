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
            viewBus();
            cbBusType.Items.Add("Bus");
            cbBusType.Items.Add("Ssamsung");
            cbBusType.Items.Add("Pick Up");
            cbBusType.Items.Add("Big Bus");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if(btnAddNew.Text=="Add New")
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
            cbBusType.Text = "";
        }

        private void viewBus()
        {
            int status = 0;
            OracleCommand cmd = new OracleCommand("showBus", conn);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add("bstatus", status);
            OracleDataAdapter adapter= new OracleDataAdapter(cmd);
            DataSet dataSet= new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource= dataSet.Tables[0];


        }
    }
}
