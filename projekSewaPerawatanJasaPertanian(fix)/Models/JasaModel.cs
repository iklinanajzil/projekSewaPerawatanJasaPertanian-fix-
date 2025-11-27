//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace projekSewaPerawatanJasaPertanian_fix_.Models
//{
//    public class JasaModel
//    {
//        // Dari JadwalJasa (PENTING: Ini adalah ID yang akan diproses saat Checkout)
//        // ID yang digunakan untuk proses transaksi/checkout
//        public int IdJadwal { get; set; }

//        // Data Jasa
//        public string NamaJasa { get; set; }
//        public decimal HargaJasa { get; set; }

//        // Data Jadwal
//        public string HariTersedia { get; set; }
//        public DateTime TanggalTersedia { get; set; }
//        // Hasil gabungan dari jam_mulai dan jam_akhir (misal: "09:00 - 11:00")
//        public string JamTersedia { get; set; }
//        public string StatusKetersediaan { get; set; } // "Tersedia" atau "Penuh"'

//        public string DeskripsiJasa { get; set; }
//        public string SimbolJasa { get; set; }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{
    public class JasaModel
    {
        // Dari JadwalJasa - Kunci untuk Checkout
        public int IdJadwal { get; set; }

        // Data Jasa
        public string NamaJasa { get; set; }
        public decimal HargaJasa { get; set; }
        public string DeskripsiJasa { get; set; }
        public string SimbolJasa { get; set; } // Ditambahkan sesuai request

        // Data Jadwal
        public string HariTersedia { get; set; }
        public DateTime TanggalTersedia { get; set; }
        // Hasil gabungan dari jam_mulai dan jam_akhir (misal: "09:00 - 11:00")
        public string JamTersedia { get; set; }
        public string StatusKetersediaan { get; set; } // "Tersedia" atau "Penuh"
    }
}