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
    public partial class AddTravelScheduleForm : Form
    {
        public AddTravelScheduleForm()
        {
            InitializeComponent();
        }

        private void AddTravelScheduleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
