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
using projekSewaPerawatanJasaPertanian_fix_.Controllers;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    public partial class AdminKelolaLayananView : Form
    {
        private readonly DataServiceController _dataService = new DataServiceController();
        public AdminKelolaLayananView()
        {
            InitializeComponent();
            this.Load += AdminDashboardView_Load;
        }
    
        private void HandleJasaDataChanged(object sender, EventArgs e)
    {
        LoadAdminJasaCards(); 
    }
    

     public void AdminDashboardView_Load(object sender, EventArgs e)
        {
            LoadAdminJasaCards();
        }

        public void LoadAdminJasaCards()
        {
          
            flowLayoutPanelJasa.Controls.Clear();

            try
            {
                List<JasaModel> jasaTersedia = _dataService.GetAllDataJasaWithJadwal();

                if (jasaTersedia != null && jasaTersedia.Any())
                {
                    flowLayoutPanelJasa.SuspendLayout(); 

                    foreach (JasaModel jasa in jasaTersedia)
                    {
                        AdminJasaCardControl card = new AdminJasaCardControl(jasa);                
                        card.DataChanged += HandleJasaDataChanged;
                        flowLayoutPanelJasa.Controls.Add(card);
                    }

                    flowLayoutPanelJasa.ResumeLayout(); 
                }
                else
                {
                    MessageBox.Show("Tidak ada data layanan jasa yang ditemukan.", "Informasi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data layanan: {ex.Message}", "Error Database");
            }
        }

      
        private void Card_EditClicked(object sender, EventArgs e)
        {
            AdminJasaCardControl card = sender as AdminJasaCardControl;
            if (card != null && card.CurrentJasa != null)
            {             
                FormEditJasa editForm = new FormEditJasa(card.CurrentJasa);
    
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAdminJasaCards(); 
                }
            }
        }

        private void Card_DeleteClicked(object sender, EventArgs e)
        {
            AdminJasaCardControl card = sender as AdminJasaCardControl;
            if (card != null && card.CurrentJasa != null)
            {
                int idJasaToDelete = card.CurrentJasa.IdJasa;
                DialogResult dialogResult = MessageBox.Show(
                    $"Apakah Anda yakin ingin menghapus SELURUH Layanan Jasa: {card.CurrentJasa.NamaJasa} beserta semua jadwalnya?",
                    "Konfirmasi Hapus Layanan Jasa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                      
                        _dataService.DeleteJasa(idJasaToDelete); 

                        MessageBox.Show("Layanan Jasa dan semua jadwal terkait berhasil dihapus.", "Sukses");

                        LoadAdminJasaCards();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menghapus jasa: " + ex.Message, "Error Database");
                    }
                }
            }
        }

            

        private void linkDashboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminDashboardView admin = new AdminDashboardView();
            admin.Show();
            this.Hide();
        }

        private void linkRiwayatTransaksi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminRiwayatTransaksiView admin = new AdminRiwayatTransaksiView();
            admin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginView login = new LoginView(); 
            login.Show();
            this.Hide();
        }

        private void btnTambahLayanan_Click(object sender, EventArgs e)
        {
            FormTambahDataJasa tambahJasaForm = new FormTambahDataJasa();

            if (tambahJasaForm.ShowDialog() == DialogResult.OK)
            {
                LoadAdminJasaCards(); 
            }
        }

        private void btnTambahJasa_Click(object sender, EventArgs e)
        {
            FormTambahJadwal tambahJadwalForm = new FormTambahJadwal();

            if (tambahJadwalForm.ShowDialog() == DialogResult.OK)
            {
                LoadAdminJasaCards(); 
            }
        }
    }
}