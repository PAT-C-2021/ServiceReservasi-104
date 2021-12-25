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
        string koneksi = "Data Source=LAPTOP-GTRP6JSV;Initial Catalog=WCFReservasi;Persist Security Info=True; User ID=sa;password=nova2000";
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

        public string pemesanan(int IDPemensanan, string NamaCustomer, string NoTelepon, int JumlahPesanan, string IDLokasi)
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

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "SELECT Kategori FROM dbo.login WHERE Username= '" + username + "' AND Password= '" + password +"'";
            conn = new SqlConnection(koneksi);
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }
            conn.Close();
                            
            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try 
            {
                string sql = "INSERT INTO dbo.login VALUES('" +username+ "','"+password+ "','"+kategori+"')";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return "Sukses";
            }
            catch(Exception e) 
            { 
                return e.ToString(); 
            }
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try 
            {
                string sql = "UPDATE dbo.login SET Username='"+username+ "',Password='"+password+ "', Kategori='"+kategori+ "' WHERE Id_login='"+id+"'";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return "Sukses";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteRegister(string username)
        {
            try 
            {
                int id = 0;
                string sql = "SELECT Id_login FROM dbo.login WHERE Username='" + username + "'";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                conn.Close();

                string sql2 = "DELETE FROM dbo.login WHERE Id_login='" + id + "'";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql2, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return "Sukses";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public List<DataRegister> DataRegisters()
        {
            List<DataRegister> list = new List<DataRegister>();
            try 
            {
                string sql = "SELECT Id_login, Username, Password, Kategori FROM dbo.login";
                conn = new SqlConnection(koneksi);
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return list;
        }

        public List<Pemesanan> Pemesanans()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "SELECT Id_reservasi, Nama_customer, No_telepon, Jml_pemesan, Nama_lokasi FROM dbo.pemesanan JOIN dbo.lokasi ON dbo.pemesanan.Id_lokasi = dbo.lokasi.Id_lokasi";                
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
                    data.Lokasi = reader.GetString(4);

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
