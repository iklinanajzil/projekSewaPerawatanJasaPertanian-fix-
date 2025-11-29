using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{
    //public class CheckoutModel
    //{
    //    public int IdJadwal { get; set; }
    //    public int IdJasa { get; set; }
    //    public decimal TotalHarga { get; set; } 

    //    public int IdPengguna { get; set; } 
    //    public string NamaPenerima { get; set; }
    //    public string NomorHP { get; set; }
    //    public string AlamatLengkap { get; set; }

    //    public int IdKelurahan { get; set; }
    //    public int IdKecamatan { get; set; }

    //    public string MetodePembayaran { get; set; }
    //    public string Status { get; set; } = "Pending";
    //}

    // File: Models/CheckoutModel.cs

    public class CheckoutModel
    {
        // Data Pelanggan & Alamat
        public int IdPengguna { get; set; }
        public string NamaPenerima { get; set; }
        public string NomorHP { get; set; }
        public string AlamatLengkap { get; set; }
        public int IdKecamatan { get; set; }
        public int IdKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string NamaKecamatan { get; set; }

        public string NamaLayanan { get; set; }
        public TimeSpan JamMulai { get; set; }
        public DateTime TanggalJadwal { get; set; }

        // Data Jasa & Jadwal
        public int IdJasa { get; set; }
        public int IdJadwal { get; set; }
        public decimal TotalHarga { get; set; }

        // Data Pembayaran & Status
        public string MetodePembayaran { get; set; }
        public string Status { get; set; }
        // Untuk pembayaran Tunai, BuktiPembayaranFilePath akan null/kosong
        public string BuktiPembayaranFilePath { get; set; }




        public TimeSpan JamAkhir { get; set; } // Baru ditambahkan di FormCheckout

    }
}
