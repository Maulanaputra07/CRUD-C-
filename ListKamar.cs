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
    public partial class ListKamar : UserControl
    {
        ConnectionSql con = new ConnectionSql();
        SqlCommand cmd;
        SqlDataReader sdr;

        int numGb = 0;
        public ListKamar()
        {
            InitializeComponent();
        }

        private void DynamicGb()
        {
            ConnectionSql.kon.Open();
            cmd = new SqlCommand("select Kamar.IDKamar, Kamar.NomorKamar, Kamar.Lantai, TipeKamar.NamaTipeKamar as TipeKamar, Kamar.hargaKamar ,Kamar.image_kamar from Kamar INNER JOIN TipeKamar ON kamar.IDTipeKamar = TipeKamar.IDTipeKamar where statusKamar = 'kosong'", ConnectionSql.kon);
            sdr = cmd.ExecuteReader();

            int padding = 0;

            while (sdr.Read())
            {
                string nomorKamar = Convert.ToString(sdr["NomorKamar"]);
                string lantai = Convert.ToString(sdr["Lantai"]);
                string tipeKamar = Convert.ToString(sdr["TipeKamar"]);
                int idKamar = int.Parse(sdr["IDKamar"].ToString());
                /*MessageBox.Show(tipeKamar);*/
                int harga = Convert.ToInt32(sdr["hargaKamar"]);
                byte[] img = (byte[])sdr["image_kamar"];

                GroupBox gb = new GroupBox();
                /*gb.Text = $"groubBox ke- {numGb} heightnya - {padding}";*/
                gb.Location = new System.Drawing.Point(10, padding);
                gb.Size = new System.Drawing.Size(520, 150);


                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                MemoryStream ms = new MemoryStream(img);
                pb.Image = Image.FromStream(ms);
                pb.Size = new Size(150, 120);
                pb.Location = new Point(10, 20);

                Label labelNk = new Label();
                labelNk.Text = $"nomor kamar : {nomorKamar}";
                labelNk.Location = new Point(189, 40);

                Label labelLk = new Label();
                labelLk.Text = $"Lantai kamar : {lantai}";
                labelLk.Location = new Point(189, 60);

                Label labelTk = new Label();
                labelTk.Text = $"{tipeKamar}";
                labelTk.Location = new Point(190, 85);

                Label labelHk = new Label();
                labelHk.Text = $"Harga : {harga}";
                labelHk.Location = new Point(190, 115);
                /*gb.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;*/

                Button detail = new Button();
                detail.Text = "Detail";
                detail.Location = new Point(330, 40);
                detail.Size = new Size(90, 40);
                detail.Anchor = AnchorStyles.Right;
                detail.Tag = idKamar;
                detail.Click += btnDetail;

                Button booking = new Button();
                booking.Text = "Booking";
                booking.Location = new Point(330, 80);
                booking.Size = new Size(90, 40);
                booking.Anchor = AnchorStyles.Right;
                booking.Tag = idKamar;
                booking.Click += btnBooking;


                gb.Controls.Add(pb);
                gb.Controls.Add(labelLk);
                gb.Controls.Add(labelTk);
                gb.Controls.Add(labelNk);
                gb.Controls.Add(labelHk);

                gb.Controls.Add(detail);
                gb.Controls.Add(booking);

                padding += 180;
                panel1.Controls.Add(gb);

                /*MessageBox.Show(nomorKamar + lantai + tipeKamar + "harga kamar : " + harga);*/
            }
            sdr.Close();
            ConnectionSql.kon.Close();
        }


        private void btnDetail(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                /*MessageBox.Show("id kamar = " + btn.Tag);*/
                DetailKamar dk = new DetailKamar(btn.Tag);
                dk.ShowDialog();
            }
        }


        private void btnBooking(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                /*MessageBox.Show("Booking Kamar");*/
                BookingKamar bk = new BookingKamar(btn.Tag);
                bk.ShowDialog();
                /* DetailKamar dk = new DetailKamar(btn.Tag);
                 dk.ShowDialog();*/
            }
        }

        private void ListKamar_Load(object sender, EventArgs e)
        {
            DynamicGb();
        }
    }
}
