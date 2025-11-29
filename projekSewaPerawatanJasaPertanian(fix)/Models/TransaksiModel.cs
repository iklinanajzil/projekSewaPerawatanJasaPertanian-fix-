using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{
    public class TransaksiModel
    {
        public int IdPengguna { get; set; }
        public string NamaPenerima { get; set; }
        public string NoHp { get; set; }
        public string AlamatLengkap { get; set; }
        public int IdKelurahan { get; set; }
        public int IdKecamatan { get; set; }
        public string MetodePembayaran { get; set; }
        public decimal TotalBayar { get; set; }
        public string Status { get; set; }   // enum: diproses/sedang berlangsung/selesai/dibatalkan

        // Tambahan penting
        public string BuktiPembayaran { get; set; }  // boleh null

        // Nanti di-return setelah insert
        public int IdTransaksi { get; set; }
    }
}
