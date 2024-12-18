using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SepanHotel
{
    internal class ConnectionSql
    {
        SqlConnection con;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataTable dt;
        SqlCommand cmd;

        public static SqlConnection kon;

        public ConnectionSql()
        {
            string conStr = @"Data Source = LAPTOP-P4298LE1\SQLEXPRESS01; Initial Catalog = hotelSepan; Integrated Security = true;";
            con = new SqlConnection(conStr);
            kon = new SqlConnection(conStr);
        }

        public void OpenConn()
        {
            if (con.State  != ConnectionState.Open)
            { con.Open(); }
        }

        public void CloseConn()
        {
            if (con.State != ConnectionState.Closed)
            { con.Close(); }
        }

        public DataTable dataTable(string query)
        {
            try
            {
                cmd = new SqlCommand(query, con);
                dt = new DataTable();
                OpenConn();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                CloseConn();
                return dt;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void Insert(SqlCommand cmd, string message)
        {
            cmd.Connection = con;
            OpenConn();
            cmd.ExecuteNonQuery();
            CloseConn();
            MessageBox.Show(message);
        }

        public int GetIntValue(string query, string col)
        {
            try
            {
                int val = 0;
                OpenConn();
                cmd = new SqlCommand(query, con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    val = sdr.GetInt32(sdr.GetOrdinal(col));
                }
                sdr.Close();
                CloseConn() ;
                return val; 

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void delete(SqlCommand cmd, string message)
        {
            try
            {
                cmd.Connection = con;
                OpenConn();
                cmd.ExecuteNonQuery();
                CloseConn();
                MessageBox.Show(message);

            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void update(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = con;
                OpenConn();
                cmd.ExecuteNonQuery();
                CloseConn();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /*public byte[] GetByteImage(string query, string col)
        {
            try
            {
                byte[] value = null;
                OpenConn();
                cmd = new SqlCommand(query, con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    value = (byte[])(sdr[col]);
                }
                sdr.Close();
                CloseConn();
                return value;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }*/
    }
}
