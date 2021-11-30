using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{    
    [ServiceContract]
    public interface IService1
    {       
        [OperationContract]
        string pemesanan(int IDPemensanan, string NamaCustomer, string NoTelepon, int JumlahPesanan, int IDLokasi);
        [OperationContract]
        string editPemesanan(string IDPemesanan, string NamaCustomer);
        [OperationContract]
        string deletePemesanan(string IDPemesanan);

        [OperationContract]
        List<CekLokasi> ReviewLokasis();
        [OperationContract]
        List<DetailLokasi> DetailLokasis();
        [OperationContract]
        List<Pemesanan> Pemesanans();
    }

    [DataContract]
    public class CekLokasi
    {
        [DataMember]
        public int IDLokasi { get; set; }
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DesripsiSingkat { get; set; }
    }

    [DataContract]
    public class DetailLokasi
    {
        [DataMember]
        public int IDLokasi { get; set; }
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DesripsiFull { get; set; }
        [DataMember]
        public int Kuota { get; set; }
    }

    [DataContract]
    public class Pemesanan
    {
        [DataMember]
        public int IDPemesanan { get; set; }
        [DataMember]
        public string NamaCustomer { get; set; }
        [DataMember]
        public string NoTelepon { get; set; }
        [DataMember]
        public int JumlahPemesanan { get; set; }
        [DataMember]
        public int IDLokasi { get; set; }
    }

}
