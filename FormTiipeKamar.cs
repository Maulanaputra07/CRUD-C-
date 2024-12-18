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
    public partial class FormTiipeKamar : UserControl
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();
        SqlCommand cmd;

        int id = 0; 
        public FormTiipeKamar()
        {
            InitializeComponent();
        }

        private void FormTiipeKamar_Load(object sender, EventArgs e)
        {
            Loaded();
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            update.Text = "Update";
            update.Name = "Update";
            update.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(update);

            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.Text = "Delete";
            delete.Name = "Delete";
            delete.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(delete);
        }

        private bool ValidationFields()
        {
            var strgs = new List<string>()
            {
                tbNama.Text,
                rtbDeskripsi.Text,
                rtbFasilitas.Text,
            };

            var validString = hlp.StringValidation(strgs);

            if (!validString)
            {
                MessageBox.Show("semua data harus terisi");
                return false;
            }

            if (numJumlah.Value < 1)
            {
                MessageBox.Show("jumlah kamar tidak dapat kurang dari satu");
                return false;
            }

            return true;
        }

        private void Loaded()
        {
            DataTable dt = con.dataTable("select NamaTipeKamar, Deskripsi, Fasilitas, JumlahKamar from TipeKamar");
            dataGridView1.DataSource = dt;
            tbNama.Text = "";
            rtbDeskripsi.Text = "";
            numJumlah.Value = 1;
            rtbFasilitas.Text = "";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var valid = ValidationFields();
            if (!valid) return; 

            if (id == 0)
            {
                cmd = new SqlCommand($"insert into TipeKamar(NamaTipeKamar, Deskripsi, JumlahKamar, fasilitas) values ('{tbNama.Text}', '{rtbDeskripsi.Text}', {numJumlah.Value}, '{rtbFasilitas.Text}')");
                con.Insert(cmd, "berhasil menambahkan tipe kamar");
                Loaded();
            }else
            {
                cmd = new SqlCommand($"update TipeKamar set NamaTipeKamar = '{tbNama.Text}', Deskripsi = '{rtbDeskripsi.Text}', JumlahKamar = '{numJumlah.Value}', Fasilitas = '{rtbFasilitas.Text}' where IDTipeKamar = {id}");
                con.Insert(cmd, "berhasil update");
                btnInsert.Text = "Simpan";
                Loaded();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                string nama = dataGridView1.CurrentRow.Cells["NamaTipeKamar"].Value.ToString();
                id = con.GetIntValue($"select IDTipeKamar from TIpeKamar where NamaTipeKamar = '{nama}'", "IDTipeKamar");
                
                if (e.ColumnIndex == 0)
                {
                    MessageBox.Show("update");


                    DataRow row = con.dataTable($"select * from TipeKamar where IDTipeKamar = {id}").Rows[0];
                    tbNama.Text = row["NamaTipeKamar"].ToString();
                    rtbDeskripsi.Text = row["Deskripsi"].ToString();
                    numJumlah.Value = int.Parse(row["JumlahKamar"].ToString());
                    rtbFasilitas.Text = row["fasilitas"].ToString();

                    btnInsert.Text = "Ubah";
                }
                else if(e.ColumnIndex == 1)
                {
                    DialogResult result = MessageBox.Show("apakah anda ingin menghapus data ini ?", "TipeKamar", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd = new SqlCommand($"delete from TipeKamar where IDTipeKamar = {id} ");
                        con.delete(cmd, "telah berhasil menghapus");
                        Loaded();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
