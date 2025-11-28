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
    public partial class FormTambahDataJasa : Form
    {
        private readonly DataServiceController _dataService = new DataServiceController();

        public FormTambahDataJasa()
        {
            InitializeComponent();
            this.Text = "Tambah Layanan Jasa Baru";

        }
        private void btnTambahJasa_Click_1(object sender, EventArgs e)
        {         
            decimal harga;
            if (!decimal.TryParse(txtHarga.Text, out harga))
            {
                MessageBox.Show("Harga harus diisi dengan angka yang valid (misal: 500000 atau 500000.00).", "Input Invalid");
                return; 
            }

            if (harga <= 0)
            {
                MessageBox.Show("Harga harus lebih besar dari nol.", "Input Invalid");
                return;
            }

            try
            {
                _dataService.InsertJasaHeaderOnly(
                    txtNamaJasa.Text,
                    txtDeskripsiJasa.Text,
                    txtIconJasa.Text,
                    harga
                );

                MessageBox.Show("Data Layanan Jasa berhasil ditambahkan. Anda sekarang dapat menambahkan jadwal untuk jasa ini.", "Sukses");
                this.DialogResult = DialogResult.OK; 
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambah Layanan Jasa: {ex.Message}", "Error Database");
            }

        }

        private void btnBatal_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
    }   
}
