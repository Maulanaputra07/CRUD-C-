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

namespace SepanHotel
{
    public partial class Form1 : Form
    {
        ConnectionSql con = new ConnectionSql();
        public Form1()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            tbEmail.Text = "";
            tbPassword.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = con.dataTable($"select * from Users where email = '{tbEmail.Text}'");
                if ( dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (tbPassword.Text == row["Password"].ToString())
                    {
                        User.id_user = int.Parse(row["IDUser"].ToString());
                        if (row["lvUser"].ToString() == "1")
                        {
                            this.Hide();
                            /*MessageBox.Show("admin");*/
                            Clear();
                            AdminNav admin = new AdminNav();
                            admin.ShowDialog();

                        } else if (row["lvUser"].ToString() == "2")
                        {
                            this.Hide();
                            /*MessageBox.Show("user");*/
                            Clear();
                            UserNav user = new UserNav();
                            user.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("password salah");
                    }
                }
                else
                {
                    MessageBox.Show("tidak terdapat akun");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           this.CenterToParent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.ShowDialog();
        }
    }
}
