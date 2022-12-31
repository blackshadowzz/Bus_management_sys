using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusMS
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }
        public void menuload(object loadNew)
        {
            if (this.menuPanel.Controls.Count > 0)
                this.menuPanel.Controls.RemoveAt(0);
            Form L = loadNew as Form;
            L.TopLevel = false;
            L.Dock = DockStyle.Fill;
            this.menuPanel.Controls.Add(L);
            this.menuPanel.Tag = L;
            L.Show();

        }
        private void Mainform_Load(object sender, EventArgs e)
        {
            
            btnUserLogin.Text=userLoginClass.getUserType();
            btnTime.Text = "Time: " + DateTime.Now.ToString();
            if (btnUserLogin.Text != "Admin")
            {
                btnEmpform.Visible = false;

                btnUserform.Visible = false;
                btncustomer.Visible = false;
                btnSetting.Visible = false;
                btnDriver.Visible = false;
                btnBus.Visible = false;
                btnDashboard.Visible = false;
                menuload(new Bookingform());
                return;
                
            }
            menuload(new Dashboardform());
        }

        private void btnUserform_Click(object sender, EventArgs e)
        {
            menuload(new Userform());
        }

        private void btnEmpform_Click(object sender, EventArgs e)
        {
            menuload(new Employeeform());
        }

        private void btnDriver_Click(object sender, EventArgs e)
        {
            menuload(new Driverform());
        }

        private void btnBus_Click(object sender, EventArgs e)
        {
            menuload(new Busform());
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            menuload(new TravelScheduleform());
        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            menuload(new Customerform());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            menuload(new Dashboardform());
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            menuload(new Bookingform());
        }

        private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBookingPay_Click(object sender, EventArgs e)
        {
            menuload(new Paymentform());
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserLogin l=new UserLogin();
            
            l.Show();
            this.Hide();

        }
    }
}
