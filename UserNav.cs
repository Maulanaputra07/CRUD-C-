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
    public partial class UserNav : Form
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        SqlDataReader sdr;
        SqlCommand cmd;

        int numGb = 0;

        public UserNav()
        {
            InitializeComponent();
        }

        

        private void UserNav_Load(object sender, EventArgs e)
        {
        }

        private void btnKamar_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new ListKamar(), panel6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new kamarUser(), panel6);
        }

        private void btnPenghuni_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
