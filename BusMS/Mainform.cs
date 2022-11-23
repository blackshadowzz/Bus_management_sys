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
    }
}
