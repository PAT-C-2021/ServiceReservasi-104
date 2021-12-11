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
        //string koneksi = "Data Source=LAPTOP-GTRP6JSV;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=nova2000";
        string koneksi = "Data Source=LAPTOP-GTRP6JSV;Initial Catalog=WCFReservasi;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;        

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "DELETE FROM dbo.pemesanan WHERE Id_reservasi = '" + IDPemesanan + "'";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                a = "sukses";
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return a;
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

                    data.IDLokasi = read.GetInt32(0);
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

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_tlp)
        {
            string a = "gagal";
            try
            {
                string sql = "UPDATE dbo.pemesanan set Nama_customer = '" + NamaCustomer + "', No_telepon='" + No_tlp + "' WHERE Id_reservasi = '" + IDPemesanan + "' ";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                a = "sukses";
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return a;
        }

        public string pemesanan(int IDPemensanan, string NamaCustomer, string NoTelepon, int JumlahPesanan, int IDLokasi)
        {
            string a = "gagal";

            try
            {
                string sql = "INSERT INTO dbo.pemesanan VALUES ('" + IDPemensanan + "', '" + NamaCustomer + "', '" + NoTelepon + "', '" + JumlahPesanan + "', '" + IDLokasi + "')";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string sql2 = "UPDATE dbo.lokasi SET Kuota = Kuota - '"+ JumlahPesanan + "' WHERE Id_lokasi = '" + IDLokasi + "'";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql2, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                a = "Sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return a;
        }        

        public List<Pemesanan> Pemesanans()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "SELECT Id_reservasi, Nama_Customer, No_telepon, Jml_pemesanan, Nama_lokasi " +
                    "FORM dbo.pemesanan p JOIN dbo.lokasi l on p.Id_lokasi = l.Id_lokasi";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();

                    data.IDPemesanan = reader.GetInt32(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelepon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetInt32(4);

                    pemesanans.Add(data);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return pemesanans;
        }

        public List<CekLokasi> ReviewLokasis()
        {
            throw new NotImplementedException();
        }
    }
}
