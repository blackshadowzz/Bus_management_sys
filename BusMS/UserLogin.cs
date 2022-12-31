using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace BusMS
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        OracleConnection conn = DBConnection.Connection();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd= new OracleCommand("userLogin",conn);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.Add("u_name",txtUsername.Text.Trim());
                    cmd.Parameters.Add("u_pass", txtPassword.Text.Trim());

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        userLoginClass.setUserID(dt.Rows[0][0].ToString());

                        userLoginClass.setUsername(dt.Rows[0]["Username"].ToString());
                        userLoginClass.setUserType(dt.Rows[0]["USER_TYPE"].ToString());
                        userLoginClass.setPw(dt.Rows[0]["Password"].ToString());

                        Mainform m = new Mainform();
                        m.Show();
                        this.Hide();
                    }
                    else
                    {
                        mess_alert.warning("Incorrect username or password!", "User Login");
                    }
                    conn.Close();
                    adapter.Dispose();

                }
                catch (Exception ex)
                {
                    mess_alert.error("Log in faild!", "Log In");
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                mess_alert.info("Please enter username and password!", "User Login");
                txtUsername.Focus();
            }
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "admin";
            txtPassword.Text = "123";
        }
    }
}
