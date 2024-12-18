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
    public partial class Register : Form
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();
        SqlCommand cmd;
        public Register()
        {
            InitializeComponent();
        }

        private bool ValidationFields()
        {
            var strgs = new List<string>() 
            {
               tbNama.Text, 
               tbEmail.Text,
               tbPassword.Text,
               richTextBox1.Text
            };

            var stringsValid = hlp.StringValidation(strgs);

            if (!stringsValid)
            {
                MessageBox.Show("semua data harus terisi");
                return false;
            }

            if (dateTimePicker1.Value >= DateTime.Today)
            {
                MessageBox.Show("tanggal lahir harus kurang dari hari ini");
                return false;
            }

            if (tbPassword.Text.Length != 8)
            {
                MessageBox.Show("password harus berjumlah 8 karakter");
                return false;
            }

            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var valid = ValidationFields();
    
            if (!valid)
            {
                return;
            }
            else
            {
                DataTable dt = con.dataTable($"select * from Users where email = '{tbEmail.Text}'");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("email telah terdaftar");
                }
                else
                {
                    
                    cmd = new SqlCommand($"insert into Users(nama, email, password, TglLahir, Alamat, lvUser) values('{tbNama.Text}', '{tbEmail.Text}', '{tbPassword.Text}' , '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{richTextBox1.Text}', 2)");
                    con.Insert(cmd, "berhasil register");
                    this.Hide();
                    Form1 login = new Form1();
                    login.ShowDialog();
                }

            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            this.CenterToScreen();
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
        }
    }
}
