using projekSewaPerawatanJasaPertanian_fix_.Controllers;
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

namespace projekSewaPerawatanJasaPertanian_fix_.Views.Admin_View
{
    public partial class FormEditJasa : Form
    {
        private readonly DataServiceController _dataService = new DataServiceController();
        private readonly JasaModel _jasaToEdit; 

        public FormEditJasa(JasaModel jasa)
        {
            InitializeComponent();
            _jasaToEdit = jasa;
            this.Text = $"Edit Layanan Jasa: {jasa.NamaJasa}";
            LoadDataJasa(); 
        }

        private void LoadDataJasa()
        {
            txtNamaLayanan.Text = _jasaToEdit.NamaJasa;
            txtDeskripsi.Text = _jasaToEdit.DeskripsiJasa;
            txtIconEmoji.Text = _jasaToEdit.SimbolJasa;
            txtHarga.Text = _jasaToEdit.HargaJasa.ToString();
            dtpTanggal.Value = _jasaToEdit.TanggalJadwal.Date;
            txtJamMulai.Text = _jasaToEdit.JamMulai.ToString(@"hh\:mm");
            txtJamAkhir.Text = _jasaToEdit.JamAkhir.ToString(@"hh\:mm");

            txtSlotKetersediaan.Text = _jasaToEdit.SlotKetersediaan.ToString();
        }


        private void btnUpdateLayanan_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaLayanan.Text) || string.IsNullOrWhiteSpace(txtDeskripsi.Text))
            {
                MessageBox.Show("Nama Layanan dan Deskripsi tidak boleh kosong.", "Validasi Input");
                return;
            }

            if (!decimal.TryParse(txtHarga.Text, out decimal hargaBaru))
            {
                MessageBox.Show("Harga harus diisi dengan angka yang valid.", "Validasi Input");
                return;
            }
            if (hargaBaru <= 0)
            {
                MessageBox.Show("Harga harus lebih besar dari nol.", "Validasi Input");
                return;
            }

            TimeSpan jamMulaiBaru, jamAkhirBaru;
            if (!TimeSpan.TryParse(txtJamMulai.Text, out jamMulaiBaru))
            {
                MessageBox.Show("Format Jam Mulai harus valid (HH:MM).", "Validasi Input");
                return;
            }
            if (!TimeSpan.TryParse(txtJamAkhir.Text, out jamAkhirBaru))
            {
                MessageBox.Show("Format Jam Akhir harus valid (HH:MM).", "Validasi Input");
                return;
            }
            if (jamMulaiBaru >= jamAkhirBaru)
            {
                MessageBox.Show("Jam Mulai harus lebih awal dari Jam Akhir.", "Validasi Input");
                return;
            }

            if (!int.TryParse(txtSlotKetersediaan.Text, out int slotBaru) || slotBaru < 0)
            {
                MessageBox.Show("Slot ketersediaan harus angka non-negatif.", "Validasi Input");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin MENGUBAH data Layanan dan jadwal ini?",
                "Konfirmasi Update Data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    _dataService.UpdateJasaHeader(
                        _jasaToEdit.IdJasa,
                        txtNamaLayanan.Text,
                        txtDeskripsi.Text,
                        txtIconEmoji.Text,
                        hargaBaru
                    );

                    _dataService.UpdateJadwal(
                        _jasaToEdit.IdJadwal,
                        _jasaToEdit.IdJasa,
                        dtpTanggal.Value.DayOfWeek.ToString(),
                        dtpTanggal.Value.Date,
                        jamMulaiBaru,
                        jamAkhirBaru,
                        slotBaru
                    );

                    MessageBox.Show("Data Layanan dan jadwal berhasil diperbarui.", "Sukses");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal memperbarui data: {ex.Message}", "Error Database");
                }
            }
        }


        private void btnBatal_Click(object sender, EventArgs e)
        {
            KonfirmasiBatal();
        }

        private void KonfirmasiBatal()
        {
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin membatalkan? Perubahan yang belum disimpan akan hilang.",
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
    }
}

 

