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
            string[] times = { "7:00 AM", "7:30 AM","8:00 AM" };
            foreach (string t in times) {
                f.cbStartTime.Items.Add(t);
                f.cbArrivelTime.Items.Add(t);
            }
            
            //f.cbStartTime.Items.Add("7:00 AM");
            //f.cbStartTime.Items.Add("7:00 AM");
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {

            addTime();
            f.ShowDialog();
        }

    }
}
