using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string koneksi = "Data Source=LAPTOP-GTRP6JSV;Initial Catalog=WCFReservasi;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;        

        public string deletePemesanan(string IDPemesanan)
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasis()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "SELECT Id_lokasi, Nama_lokasi, Full_Deskripsi, Kuota FROM dbo.lokasi";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    DetailLokasi data = new DetailLokasi();

                    data.IDLokasi = read.GetString(0);
                    data.NamaLokasi = read.GetString(1);
                    data.DesripsiFull = read.GetString(3);
                    data.Kuota = read.GetInt32(4);
                    LokasiFull.Add(data);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return LokasiFull;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer)
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string pemesanan(string IDPemensanan, string NamaCustomer, string NoTelepon, int JumlahPesanan, string IDLokasi)
        {
            string a = "gagal";

            try
            {
                string sql = "INSERT INTO dbo.pemesanan VALUES('"+ IDPemensanan+"','"+NamaCustomer+ "','"+NoTelepon+ "','"+JumlahPesanan+ "','"+IDLokasi+"',)";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                a = "Sukses";
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return a;
        }

        public List<Pemesanan> Pemesanans()
        {
            throw new NotImplementedException();
        }

        public List<CekLokasi> ReviewLokasis()
        {
            throw new NotImplementedException();
        }
    }
}
