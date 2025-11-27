//using projekSewaPerawatanJasaPertanian_fix_.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
//{
//    public partial class JasaCardControl : UserControl
//    {
//        public JasaCardControl()
//        {
//            InitializeComponent();
//        }

//        // Method untuk mengisi data dari JasaModel
//        public void SetData(JasaModel jasa)
//        {
//            // Menyimpan ID Jadwal di Tag
//            this.Tag = jasa.IdJadwal;

//            lblNamaJasa.Text = jasa.NamaJasa;
//            // Format mata uang (contoh)
//            lblHargaJasa.Text = $"Rp {jasa.HargaJasa:N0}";

//            // Tampilkan Hari dan Tanggal
//            lblTanggalJadwal.Text = $"{jasa.HariTersedia}, {jasa.TanggalTersedia:dd MMMM yyyy}";

//            // Tampilkan Jam Tersedia (sudah digabung dari SQL)
//            lblJamJadwal.Text = $"Pukul {jasa.JamTersedia}";

//            lblStatusSlot.Text = $"Status: {jasa.StatusKetersediaan}";

//            lblSimbolJasa.Text = jasa.SimbolJasa;

//            // Label Deskripsi
//            lblDeskripsi.Text = jasa.DeskripsiJasa;

//            // Tampilan CheckBox: pastikan chkPilihJasa ada di Designer
//            chkPilihJasa.Checked = false;
//        }

//        // Properti untuk mengetahui apakah card ini dipilih
//        public bool IsSelected
//        {
//            get { return chkPilihJasa.Checked; }
//        }
//    }
//}

using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
{
    public partial class JasaCardControl : UserControl
    {
        public JasaCardControl()
        {
            InitializeComponent();
        }

        public void SetData(JasaModel jasa)
        {
            this.Tag = jasa.IdJadwal;

            // Pastikan lblNamaJasa, lblHargaJasa, lblSimbolJasa, dll., adalah nama kontrol yang benar di Designer
            lblNamaJasa.Text = jasa.NamaJasa;
            lblHargaJasa.Text = $"Rp {jasa.HargaJasa:N0}";
            lblTanggalJadwal.Text = $"{jasa.HariTersedia}, {jasa.TanggalTersedia:dd MMMM yyyy}";
            lblJamJadwal.Text = $"Pukul {jasa.JamTersedia}";
            lblStatusSlot.Text = $"Status: {jasa.StatusKetersediaan}";
            lblSimbolJasa.Text = jasa.SimbolJasa;
            lblDeskripsi.Text = jasa.DeskripsiJasa;
            chkPilihJasa.Checked = false;
        }

        public bool IsSelected
        {
            get { return chkPilihJasa.Checked; }
        }

        private void lblJamJadwal_Click(object sender, EventArgs e)
        {

        }
    }
}
