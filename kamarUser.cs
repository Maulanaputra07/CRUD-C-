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
    public partial class kamarUser : UserControl
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        DataTable dt;
        SqlCommand cmd;
        SqlDataReader sdr;

        int rows = 0;

        public kamarUser()
        {
            InitializeComponent();
        }

        private void kamarUser_Load(object sender, EventArgs e)
        {
            try
            {

            dt = con.dataTable($"select pemesanan.id_pemesanan as no, kamar.NomorKamar, Kamar.Lantai, TipeKamar.NamaTipeKamar as TipeKamar, TipeKamar.fasilitas, pemesanan.nama_pemesan, pemesanan.check_in, pemesanan.check_out, FasilitasTambahan.NamaFasilitasTambahan, pemesanan.total_harga FROM pemesanan INNER JOIN Kamar ON Pemesanan.id_kamar = Kamar.idKamar INNER JOIN TipeKamar ON Kamar.idTipeKamar = TipeKamar.IDTipeKamar LEFT JOIN FasilitasTambahan ON Pemesanan.id_fasilitasTambahan = FasilitasTambahan.IDFasilitasTambahan WHERE id_user = {User.id_user} AND Kamar.statusKamar = 'dipesan' ;");

            dataGridView1.DataSource = dt;
            DataGridViewButtonColumn co = new DataGridViewButtonColumn();
            co.Name = "Check_out";
            co.UseColumnTextForButtonValue = true;

            ConnectionSql.kon.Open();
            cmd = new SqlCommand($"select kamar.statusKamar from pemesanan inner join kamar ON pemesanan.id_kamar = kamar.IDKamar where id_user = {User.id_user}", ConnectionSql.kon);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                string status = sdr["statusKamar"].ToString();
                if(status == "kosong")
                {
                    /*MessageBox.Show("");*/
                    co.Text = "✔️";
                }
                else
                {
                    co.Text = "Check_out";
                }/*

                dataGridView1.Rows[rowIndex].Cells["Check_out"].Value = co.Text;
                rowIndex++;*/
                /*MessageBox.Show("staus kamar " + status);*/
            }
            sdr.Close();
            ConnectionSql.kon.Close();

            dataGridView1.Columns.Add(co);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                int id_pemesanan = int.Parse(dataGridView1.CurrentRow.Cells["no"].Value.ToString());
                int id_kamar = con.GetIntValue($"select Id_kamar from pemesanan where id_pemesanan = {id_pemesanan}", "id_kamar");

                MessageBox.Show("id kamar : " + id_kamar);

                if (e.ColumnIndex == 0)
                {
                    /*MessageBox.Show("check_out");*/
                    cmd = new SqlCommand($"update Kamar set statusKamar = 'kosong' where IDKamar = {id_kamar}");


                    con.Insert(cmd, "berhasil check-out");
                }
            }
        }
    }
}
