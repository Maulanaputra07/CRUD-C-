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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SepanHotel
{
    public partial class FormKamar : UserControl
    {

        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        SqlCommand cmd;
        SqlDataReader sdr;

        int id = 0;
        public FormKamar()
        {
            InitializeComponent();
        }

        public void Loaded()
        {
            dataGridView1.DataSource = con.dataTable("select Kamar.NomorKamar, Kamar.Lantai, TipeKamar.NamaTipeKamar, Kamar.hargaKamar ,Kamar.image_kamar from Kamar INNER JOIN TipeKamar ON kamar.IDTipeKamar = TipeKamar.IDTipeKamar");


            cmbTipeKamar.DataSource = con.dataTable("select * from tipeKamar");
            cmbTipeKamar.ValueMember = "IDTipeKamar";
            cmbTipeKamar.DisplayMember = "NamaTipeKamar";
        }


        string imgLocation = "";

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = " png filles(*.png)|*.png|jpg filles(*.jpg)|*.jpg|All filles(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = openFileDialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;

            }
        }

        private void FormKamar_Load(object sender, EventArgs e)
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

        private bool validationFields()
        {
            var strgs = new List<string>()
            {
                tbHarga.Text,
                tbLantai.Text,
                tbNomorKamar.Text,
                cmbTipeKamar.Text,
            };

            var valStrgs = hlp.StringValidation(strgs);

            if (!valStrgs)
            {
                MessageBox.Show("semua data harus terisi");
                return false;
            }

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Gambar tidak dapat kosong");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var valid = validationFields();
            if (!valid) return;

            byte[] images = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);

            if (id == 0)
            {
                cmd = new SqlCommand($"insert into Kamar(NomorKamar, Lantai, IDTipeKamar, hargaKamar, image_kamar, statusKamar) values ('{tbNomorKamar.Text}', '{tbLantai.Text}', '{cmbTipeKamar.SelectedValue}', '{tbHarga.Text}', @img, 'kosong')");
                cmd.Parameters.AddWithValue("@img", images);
                con.Insert(cmd, "berhasil insert kamar");
                Loaded();
            }
            else
            {
                cmd = new SqlCommand($"update Kamar set NomorKamar = '{tbNomorKamar.Text}', Lantai = '{tbLantai.Text}', IDTipeKamar = '{cmbTipeKamar.SelectedValue}', hargaKamar = '{tbHarga.Text}', image_kamar = @img where IDKamar = {id} ");
                cmd.Parameters.AddWithValue("@img", images);
                con.Insert(cmd, "berhasil update kamar");
                Loaded();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int nomorKamar = int.Parse(dataGridView1.CurrentRow.Cells["NomorKamar"].Value.ToString());
                id = con.GetIntValue($"select IDKamar from Kamar where NomorKamar = {nomorKamar}", "IDKamar");


                ConnectionSql.kon.Open();
                cmd = new SqlCommand($"select image_kamar from Kamar where IDKamar = {id}", ConnectionSql.kon);

                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    byte[] img = (byte[])(sdr["image_kamar"]);

                    if (img == null || img.Length == 0)
                    {
                        /*MessageBox.Show("gagal");*/
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        /*MessageBox.Show("berhasil");*/
                        try
                        {
                            MemoryStream ms = new MemoryStream(img);
                            pictureBox1.Image = Image.FromStream(ms);
                        }catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                sdr.Close();
                ConnectionSql.kon.Close();

                if (e.ColumnIndex == 0)
                {
                    MessageBox.Show("update");

                    DataRow row = con.dataTable($"select * from Kamar where IDKamar = {id}").Rows[0];
                    tbNomorKamar.Text = row["NomorKamar"].ToString();
                    tbLantai.Text = row["Lantai"].ToString() ;
                    cmbTipeKamar.SelectedValue = int.Parse(row["IDTipeKamar"].ToString());
                    tbHarga.Text = row["HargaKamar"].ToString();
                    byte[] img = (byte[])(row["image_kamar"]);
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }else if (e.ColumnIndex == 1)
                {
                    DialogResult result = MessageBox.Show("apakah anda ingin menghapus data ini", "kamar", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        cmd = new SqlCommand($"delete from Kamar where IDKamar = {id}");
                        con.delete(cmd, "berhasil menghapus data");
                        Loaded();
                    }
                }
            }
        }
    }
}
