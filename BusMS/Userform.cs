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
    public partial class Userform : Form
    {
        public Userform()
        {
            InitializeComponent();
        }
        OracleConnection conn = DBConnection.Connection();
        private void Userform_Load(object sender, EventArgs e)
        {

        }
    }
}
