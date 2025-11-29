using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{
    public class DetailTransaksiModel
    {
        public int IdJasa { get; set; }
        public int JumlahPesanan { get; set; }
        public decimal SubtotalHarga { get; set; }
    }
}

