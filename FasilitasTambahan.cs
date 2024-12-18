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
    public partial class FasilitasTambahan : UserControl
    {
        ConnectionSql con = new ConnectionSql();
        Helper hlp = new Helper();

        SqlCommand cmd;
        int id = 0;

        public FasilitasTambahan()
        {
            InitializeComponent();
        }

        private void Loaded()
        {
            dataGridView1.DataSource = con.dataTable("select NamaFasilitasTambahan, HargaFasilitasTambahan from FasilitasTambahan");

            tbHargaFas.Text = "";
            tbNamaFas.Text = "";

        }

        private void FasilitasTambahan_Load(object sender, EventArgs e)
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
                tbHargaFas.Text, 
                tbNamaFas.Text,
            };

            var valStr = hlp.StringValidation(strgs);

            if (!valStr)
            {
                MessageBox.Show("Semua fields harus terisi");
                return false;
            }

            return true;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            var valid = ValidationFields();

            if (!valid) return;

            if (id == 0)
            {
                cmd = new SqlCommand($"insert into FasilitasTambahan(NamaFasilitasTambahan, HargaFasilitasTambahan) values ('{tbNamaFas.Text}', '{tbHargaFas.Text}')");
                con.Insert(cmd, "berhasil insert");
                Loaded();
            }
            else
            {
                cmd = new SqlCommand($"Update FasilitasTambahan set NamaFasilitasTambahan = '{tbNamaFas.Text}', HargaFasilitasTambahan = '{tbHargaFas.Text}' where IDFasilitasTambahan = {id}");
                con.Insert(cmd, "berhasil Update");
                Loaded();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string nama = dataGridView1.CurrentRow.Cells["NamaFasilitasTambahan"].Value.ToString();
                id = con.GetIntValue($"select IDFasilitasTambahan from FasilitasTambahan where NamaFasilitasTambahan = '{nama}'", "IDFasilitasTambahan");

                if (e.ColumnIndex == 0)
                {
                    MessageBox.Show("Update");
                    DataRow row = con.dataTable($"select * from FasilitasTambahan where IDFasilitasTambahan = {id}").Rows[0];
                    tbNamaFas.Text = row["NamaFasilitasTambahan"].ToString();
                    tbHargaFas.Text = row["HargaFasilitasTambahan"].ToString();
                    
                    
                }else if (e.ColumnIndex == 1)
                {
                    DialogResult result = MessageBox.Show("Apakah anda ingin menghhpaus data ini ? ", "Fasilitas tambahan", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK) ;
                    {
                        cmd = new SqlCommand($"delete from FasilitasTambahan where IDFasilitasTambahan  = {id}");
                        con.delete(cmd, "berhasil delete data");
                        Loaded();
                    }
                }
            }
        }
    }
}
