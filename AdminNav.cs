using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SepanHotel
{
    public partial class AdminNav : Form
    {
        Helper hlp = new Helper();  
        public AdminNav()
        {
            InitializeComponent();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new FormTiipeKamar(), panel6);

        }

        private void AdminNav_Load(object sender, EventArgs e)
        {
        }

        private void btnKamar_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new FormKamar(), panel6);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPenghuni_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new Penghuni(), panel6);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFasilitas_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new FasilitasTambahan(), panel6);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hlp.OpenUc(new HistoryPemesanan(), panel6);
        }
    }
}
