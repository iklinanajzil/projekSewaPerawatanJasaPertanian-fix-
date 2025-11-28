using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    public partial class FormTambahJadwal : Form
    {     
        private readonly DataServiceController _dataService = new DataServiceController();

        public FormTambahJadwal()
        {
            InitializeComponent();
            this.Text = "Tambah Jadwal Ketersediaan";
            this.Load += FormTambahJadwal_Load;
        }

        private void FormTambahJadwal_Load(object sender, EventArgs e)
        {
            LoadJasaList(); 
        }

        private void LoadJasaList()
        {
            try
            {
                DataTable dtJasa = _dataService.GetAllDataJasa();

                cmbNamaJasa.DataSource = dtJasa;
                cmbNamaJasa.DisplayMember = "nama_jasa";
                cmbNamaJasa.ValueMember = "id_jasa";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar Jasa: " + ex.Message);
            }
        }

        private void btnTambahJadwal_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbNamaJasa.SelectedValue == null || !int.TryParse(cmbNamaJasa.SelectedValue.ToString(), out int idJasa) ||
                    !int.TryParse(txtSlotKetersediaan.Text, out int slot) ||
                    !TimeSpan.TryParse(txtJamMulai.Text, out TimeSpan jamMulai) ||
                    !TimeSpan.TryParse(txtJamAkhir.Text, out TimeSpan jamAkhir))
                {
                    MessageBox.Show("Mohon lengkapi semua data Jadwal dengan format yang benar.");
                    return;
                }

                DateTime tanggal = dtpTanggal.Value;
                string hari = tanggal.DayOfWeek.ToString();

                _dataService.InsertJadwalBaru(
                    idJasa,
                    hari,
                    tanggal,
                    jamMulai,
                    jamAkhir,
                    slot
                );

                MessageBox.Show("Jadwal baru berhasil ditambahkan.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah jadwal: " + ex.Message, "Database Error");
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            KonfirmasiBatal();
        }

        private void KonfirmasiBatal()
        {
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin membatalkan? Semua data yang sudah diisi akan hilang.",
                "Konfirmasi Pembatalan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning 
            );

            
            if (result == DialogResult.Yes)
            {
                
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        private void btnTambahJadwal_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbNamaJasa.SelectedValue == null || !int.TryParse(cmbNamaJasa.SelectedValue.ToString(), out int idJasa) ||
                    !int.TryParse(txtSlotKetersediaan.Text, out int slot) ||
                    !TimeSpan.TryParse(txtJamMulai.Text, out TimeSpan jamMulai) ||
                    !TimeSpan.TryParse(txtJamAkhir.Text, out TimeSpan jamAkhir))
                {
                    MessageBox.Show("Mohon lengkapi semua data Jadwal dengan format yang benar.");
                    return;
                }
                DateTime tanggal = dtpTanggal.Value;
                string hari = tanggal.DayOfWeek.ToString(); 
                string pesanKonfirmasi = "Apakah Anda yakin ingin MENAMBAHKAN jadwal baru ini?";
                DialogResult result = MessageBox.Show(
                    pesanKonfirmasi,
                    "Konfirmasi Operasi Data",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _dataService.InsertJadwalBaru(
                            idJasa,
                          hari,
                          tanggal,
                          jamMulai,
                          jamAkhir,
                           slot   
                        );

                        MessageBox.Show("Data jadwal berhasil ditambahkan.", "Sukses");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal menyimpan data: {ex.Message}", "Error Database");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah jadwal: " + ex.Message, "Database Error");
            }

        }
    }
}
