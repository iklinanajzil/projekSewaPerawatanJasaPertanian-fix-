
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

            lblNamaJasa.Text = jasa.NamaJasa;
            lblHargaJasa.Text = $"Rp {jasa.HargaJasa:N0}";
            lblHari.Text = $"{jasa.HariTersedia}, {jasa.TanggalJadwal:dd MMMM yyyy}";
            lblJamMulai.Text = $"Pukul {jasa.JamMulai.ToString(@"hh\:mm")}";
            lblJamAkhir.Text =$" - {jasa.JamAkhir.ToString(@"hh\:mm")}";

            lblSimbolJasa.Text = jasa.SimbolJasa;
            lblDeskripsi.Text = jasa.DeskripsiJasa;

            chkPilihJasa.Checked = false;
            if (jasa.SlotKetersediaan == "0")
            {
                lblStatusSlot.Text = "Status: Tidak Tersedia / Penuh";
                lblStatusSlot.ForeColor = Color.Red;
                 
            }
            else
            {
                lblStatusSlot.Text = "Status: Tersedia";
                lblStatusSlot.ForeColor = Color.Green; 
            }   
        }

        public bool IsSelected
        {
            get { return chkPilihJasa.Checked; }
        }
    }
}