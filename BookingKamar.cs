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
    public partial class BookingKamar : Form
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        DataRow dataRow;
        SqlCommand cmd;

        int id = 0;
        int hargaFasilitas = 0;
        int hrgKmr = 0;
        

        public BookingKamar(object idKamar)
        {
            InitializeComponent();
            id = int.Parse(idKamar.ToString());
        }

        private void Loaded()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";

            comboBox1.DataSource = con.dataTable("select * from FasilitasTambahan");
            comboBox1.ValueMember = "IDFasilitasTambahan";
            comboBox1.DisplayMember = "NamaFasilitasTambahan";


            DataRow row = con.dataTable($"select NomorKamar, hargaKamar, Lantai from Kamar where IDKamar = {id}").Rows[0];
            lblNomor.Text = $"Nomor kamar : {row["NomorKamar"].ToString()}";
            lblLantai.Text = $"Lantai kamar : {row["Lantai"].ToString()}";
            lblHarga.Text = $"Harga Kamar : Rp. {row["hargaKamar"].ToString()}";
            hrgKmr = Convert.ToInt32(row["hargaKamar"]);

            lblTotal.Text = $"Rp.{hargaFasilitas + hrgKmr}";
        }

        private void BookingKamar_Load(object sender, EventArgs e)
        {
            Loaded();
        }

        private void btnPesan_Click(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            /*MessageBox.Show("IdUser" + User.id_user);*/
            cmd = new SqlCommand($"insert into Pemesanan(id_user, id_kamar, check_in, check_out, nama_pemesan, no_tlp, id_fasilitasTambahan, total_harga, tgl_pemesanan) values ({User.id_user}, {id}, '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}', '{tbNama.Text}', '{tbNoHp.Text}', '{comboBox1.SelectedValue}', {hargaFasilitas + hrgKmr}, '{now.ToString("yyyy-MM-dd")}' )");
            con.Insert(cmd, "berhasil memesan kamar");

            SqlCommand cmdUpdate = new SqlCommand($"update Kamar set statusKamar = 'dipesan' where IDKamar = {id}");
            con.update(cmdUpdate);

            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand($"select HargaFasilitasTambahan from FasilitasTambahan where NamaFasilitasTambahan = '{comboBox1.Text}'", ConnectionSql.kon);
            ConnectionSql.kon.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                hargaFasilitas = Convert.ToInt32(reader["HargaFasilitasTambahan"].ToString());
                /*MessageBox.Show("harga : " + hrga);*/
                lblFasilitas.Text = $"{comboBox1.Text}: Rp.{hargaFasilitas}";
                lblTotal.Text = $"Rp.{hargaFasilitas + hrgKmr}";
            }
            reader.Close();
            ConnectionSql.kon.Close();
        }
    }
}
