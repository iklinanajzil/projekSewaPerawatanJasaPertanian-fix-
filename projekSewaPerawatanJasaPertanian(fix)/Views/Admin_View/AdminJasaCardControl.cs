 using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{ 

    public partial class AdminJasaCardControl : UserControl
    {
        public event EventHandler DataChanged;

        public JasaModel CurrentJasa { get; private set; }

        public AdminJasaCardControl(JasaModel jasa)
        {
            InitializeComponent();
            CurrentJasa = jasa;
            LoadDataToUI(jasa);
        }

        private void LoadDataToUI(JasaModel jasa)
        {
            lblNamaJasa.Text = jasa.NamaJasa;
            lblDeskripsi.Text = jasa.DeskripsiJasa;
            lblSimbolJasa.Text = jasa.SimbolJasa;
            lblHargaJasa.Text = $"Rp {jasa.HargaJasa:N0}";

            lblTanggalJadwal.Text = $"{jasa.HariTersedia}, {jasa.TanggalJadwal:dd MMMM yyyy}";
            lblJamMulai.Text = jasa.JamMulai.ToString(@"hh\:mm");
            lblJamAkhir.Text = jasa.JamAkhir.ToString(@"hh\:mm");
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

            this.Tag = jasa.IdJadwal;
        }

       
        private void btnHapus_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus layanan dan jadwal ini?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataServiceController controller = new DataServiceController();
                    controller.DeleteJasa(CurrentJasa.IdJasa);

                    MessageBox.Show("Layanan dan jadwal berhasil dihapus.", "Sukses");
                    DataChanged?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus layanan: {ex.Message}", "Error Database");
                }
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            FormEditJasa formEdit = new FormEditJasa(CurrentJasa);
            if (formEdit.ShowDialog() == DialogResult.OK)
            {         
                DataChanged?.Invoke(this, EventArgs.Empty);
            }

        }
    }
}