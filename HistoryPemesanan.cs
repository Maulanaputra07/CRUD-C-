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
    public partial class HistoryPemesanan : UserControl
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();
        public HistoryPemesanan()
        {
            InitializeComponent();
        }

        private void HistoryPemesanan_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = con.dataTable($"select Users.nama, Users.Alamat, Kamar.NomorKamar, Kamar.Lantai, TipeKamar.NamaTipeKamar as TipeKamar, TipeKamar.fasilitas, pemesanan.nama_pemesan, pemesanan.no_tlp, pemesanan.check_in, pemesanan.check_out, pemesanan.tgl_pemesanan, FasilitasTambahan.NamaFasilitasTambahan, pemesanan.total_harga FROM pemesanan INNER JOIN Kamar ON Pemesanan.id_kamar = Kamar.idKamar INNER JOIN TipeKamar ON Kamar.idTipeKamar = TipeKamar.IDTipeKamar LEFT JOIN FasilitasTambahan ON Pemesanan.id_fasilitasTambahan = FasilitasTambahan.IDFasilitasTambahan INNER JOIN Users ON pemesanan.id_user = Users.IDUser;");
        }
    }
}
