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

namespace projekSewaPerawatanJasaPertanian_fix_.Views.User_View
{
    public partial class FormCheckout : Form
    {
        //private readonly DataServiceController _dataService = new DataServiceController();
        //private readonly List<int> _idJadwalTerpilih;
        //private readonly decimal _totalHarga;
        //private readonly int _idPengguna = 1; // Ganti dengan ID Pengguna yang sedang Login!

        //// Constructor menerima List ID Jadwal dan Total Harga dari Dashboard
        //public FormCheckout(List<int> idJadwalTerpilih, decimal totalHarga)
        //{
        //    InitializeComponent();
        //    _idJadwalTerpilih = idJadwalTerpilih;
        //    _totalHarga = totalHarga;

        //    // Tampilkan total harga ke label atau textbox yang sesuai
        //    lblTotalPembayaran.Text = $"Rp {totalHarga:N0}";
        //}

        //private void FormCheckout_Load(object sender, EventArgs e)
        //{
        //    // 1. Isi Dropdown Kecamatan
        //    LoadKecamatan();
        //}

        //private void LoadKecamatan()
        //{
        //    try
        //    {
        //        DataTable dtKecamatan = _dataService.GetAllKecamatan();

        //        // Konfigurasi ComboBox Kecamatan
        //        cmbKecamatan.DataSource = dtKecamatan;
        //        cmbKecamatan.DisplayMember = "nama_kecamatan";
        //        cmbKecamatan.ValueMember = "id_kecamatan";

        //        // Panggil LoadKelurahan untuk mengisi Kelurahan default (jika ada data)
        //        if (dtKecamatan.Rows.Count > 0)
        //        {
        //            LoadKelurahan();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Gagal memuat data Kecamatan: " + ex.Message);
        //    }
        //}

        //private void LoadKelurahan()
        //{
        //    if (cmbKecamatan.SelectedValue != null && cmbKecamatan.SelectedValue is int idKecamatan)
        //    {
        //        try
        //        {
        //            DataTable dtKelurahan = _dataService.GetKelurahanByKecamatan(idKecamatan);

        //            // Konfigurasi ComboBox Kelurahan
        //            cmbKelurahan.DataSource = dtKelurahan;
        //            cmbKelurahan.DisplayMember = "nama_kelurahan";
        //            cmbKelurahan.ValueMember = "id_kelurahan";
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Gagal memuat data Kelurahan: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        cmbKelurahan.DataSource = null;
        //    }
        //}

        //// Event saat Kecamatan diganti (Cascading Dropdown)
        //private void cmbKecamatan_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbKecamatan.SelectedValue != null && cmbKecamatan.SelectedValue is int)
        //    {
        //        LoadKelurahan();
        //    }
        //}

        //// --- Logika Tombol Lanjut ke Pembayaran (Pop-up 1 ke Pop-up 2) ---
        //private void btnLanjutPembayaran_Click(object sender, EventArgs e)
        //{
        //    // Validasi input dasar
        //    if (string.IsNullOrWhiteSpace(txtNamaPenerima.Text) ||
        //        string.IsNullOrWhiteSpace(txtNomorHp.Text) ||
        //        string.IsNullOrWhiteSpace(txtAlamatLengkap.Text) ||
        //        cmbKecamatan.SelectedValue == null || cmbKelurahan.SelectedValue == null)
        //    {
        //        MessageBox.Show("Mohon lengkapi semua data penerima dan alamat.");
        //        return;
        //    }

        //    // Tampilkan Pop-up Pemilihan Pembayaran (Anggap Anda buat Form terpisah untuk ini)
        //    // Namun, untuk menyederhanakan, anggap kita pindah ke Tab atau Panel di Form ini.

        //    // Logika untuk menampilkan Panel Pembayaran (pop-up 2)
        //    panelDetailPemesanan.Visible = false;
        //    panelPembayaran.Visible = true;
        //}

        //// --- Logika Tombol Konfirmasi Pesanan (Pop-up 2, Finalisasi) ---
        //private void btnKonfirmasiPesanan_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Ambil data dari kontrol input (harus dilakukan validasi lagi jika perlu)
        //        string namaPenerima = txtNamaPenerima.Text;
        //        string noHp = txtNomorHp.Text;
        //        string alamatLengkap = txtAlamatLengkap.Text;
        //        int idKecamatan = (int)cmbKecamatan.SelectedValue;
        //        int idKelurahan = (int)cmbKelurahan.SelectedValue;

        //        // Ambil metode pembayaran yang dipilih (Contoh: dari RadioButton)
        //        string metodePembayaran = GetSelectedMetodePembayaran();

        //        if (string.IsNullOrEmpty(metodePembayaran))
        //        {
        //            MessageBox.Show("Mohon pilih metode pembayaran.");
        //            return;
        //        }

        //        // Panggil Controller untuk menyimpan transaksi
        //        _dataService.InsertTransaksi(
        //            _idPengguna,
        //            _idJadwalTerpilih,
        //            namaPenerima,
        //            noHp,
        //            alamatLengkap,
        //            idKelurahan,
        //            idKecamatan,
        //            metodePembayaran
        //        );

        //        MessageBox.Show("Pemesanan berhasil! Silakan lakukan pembayaran sesuai instruksi.");
        //        this.Close(); // Tutup Form Checkout
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Gagal menyimpan transaksi: " + ex.Message, "Error Transaksi");
        //    }
        //}

        //// Contoh fungsi untuk mendapatkan metode pembayaran dari Radio Button/Group Box
        //private string GetSelectedMetodePembayaran()
        //{
        //    // Anda harus mengimplementasikan logika ini sesuai desain Form Anda
        //    // Contoh: Mencari RadioButton yang dicentang di GroupBox bernama gbMetodePembayaran
        //    // if (rbCash.Checked) return "Cash";
        //    // if (rbBCA.Checked) return "Transfer BCA";

        //    return "Metode Belum Dipilih"; // Placeholder
        //}
    }
}
    

