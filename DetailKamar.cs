using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SepanHotel
{
    public partial class DetailKamar : Form
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        SqlCommand cmd;

        int id = 0;
        public DetailKamar(object idKamar)
        {
            InitializeComponent();
            id = int.Parse(idKamar.ToString());
        }

        private void DetailKamar_Load(object sender, EventArgs e)
        {
            DataRow row = con.dataTable($"select Kamar.NomorKamar, Kamar.Lantai, Kamar.hargaKamar, Kamar.image_kamar, TipeKamar.NamaTipeKamar, TipeKamar.Deskripsi, TipeKamar.fasilitas, TipeKamar.JumlahKamar from Kamar INNER JOIN TipeKamar ON Kamar.IDTipeKamar = TipeKamar.IDTipeKamar where IDKamar = {id} ").Rows[0];
            lblNomor.Text = $"Nomor kamar : {row["NomorKamar"].ToString()}";
            MemoryStream ms = new MemoryStream((byte[])row["image_kamar"]);
            pictureBox1.Image = Image.FromStream(ms);
            lblLantai.Text = $"Lantai kamar : {row["Lantai"].ToString()}";
            lblTipe.Text = $"Tipe Kamer : {row["NamaTipeKamar"].ToString()}";
            lblJum.Text = $"Jumlah kamar : {row["JumlahKamar"].ToString()}";
            richTextBox1.Text = row["Deskripsi"].ToString();
            richTextBox2.Text = row["Fasilitas"].ToString();
            lblHarga.Text = $"Harga kamar : {row["hargaKamar"].ToString()}";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
