
using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Controllers;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using projekSewaPerawatanJasaPertanian_fix_.Database;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Views
{
    public partial class FormCheckout : Form
    {
        private readonly DbContext _dbContext;
        private readonly DataServiceController _controller;
        private readonly int _idPengguna;

        private readonly List<JasaModel> _jasaDipilih;
        private decimal _totalHarga = 0;

        public FormCheckout(List<JasaModel> jasaDipilih)
        {
            InitializeComponent();

            _dbContext = new DbContext();
            _controller = new DataServiceController();

            _jasaDipilih = jasaDipilih;
            //_idPengguna = userId;
        }

        private void FormCheckout_Load(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (var jasa in _jasaDipilih)
            {
                lbJasa.Items.Add($"{jasa.NamaJasa} - Rp {jasa.HargaJasa:N0}");
                total += jasa.HargaJasa;
            }

            lblTotalPembayaran.Text = $"Rp {total:N0}";
            LoadKecamatan();
        }

        private void LoadKecamatan()
        {
            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                var cmd = new NpgsqlCommand(
                    "SELECT id_kecamatan, nama_kecamatan FROM kecamatan ORDER BY nama_kecamatan",
                    conn);

                var reader = cmd.ExecuteReader();
                Dictionary<int, string> data = new Dictionary<int, string>();

                while (reader.Read())
                    data.Add(reader.GetInt32(0), reader.GetString(1));

                cmbKecamatan.DataSource = new BindingSource(data, null);
                cmbKecamatan.DisplayMember = "Value";
                cmbKecamatan.ValueMember = "Key";
            }
        }


        private void btnBatal_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
            "Apakah Anda yakin ingin membatalkan pesanan ini?",
            "Konfirmasi Pembatalan",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                // 1. Set DialogResult ke Cancel (opsional, tetapi praktik yang baik)
                this.DialogResult = DialogResult.Cancel;

                // 2. Tutup Form Checkout
                this.Close();
            }
        }

        private void cmbKecamatan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int idKec = ((KeyValuePair<int, string>)cmbKecamatan.SelectedItem).Key;

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                var cmd = new NpgsqlCommand(
                    "SELECT id_kelurahan, nama_kelurahan FROM kelurahan WHERE id_kecamatan = @id ORDER BY nama_kelurahan",
                    conn);

                cmd.Parameters.AddWithValue("@id", idKec);

                var reader = cmd.ExecuteReader();
                Dictionary<int, string> data = new Dictionary<int, string>();

                while (reader.Read())
                    data.Add(reader.GetInt32(0), reader.GetString(1));

                cmbKelurahan.DataSource = new BindingSource(data, null);
                cmbKelurahan.DisplayMember = "Value";
                cmbKelurahan.ValueMember = "Key";
            }

        }

        private void btnLanjutPembayaran_Click_1(object sender, EventArgs e)
        {
            if (txtNamaPenerima.Text == "" || txtNomorHP.Text == "" || txtAlamatLengkap.Text == "")
            {
                MessageBox.Show("Lengkapi semua data penerima.");
                return;
            }

            int idKec = ((KeyValuePair<int, string>)cmbKecamatan.SelectedItem).Key;
            int idKel = ((KeyValuePair<int, string>)cmbKelurahan.SelectedItem).Key;

            // -----------------------------
            // INSERT TRANSAKSI DAN DAPATKAN ID
            // -----------------------------
            decimal totalHarga = 0;
            foreach (var j in _jasaDipilih)
            {
                totalHarga += j.HargaJasa;
            }

            int idTransaksi = _controller.InsertTransaksi(
                _idPengguna,
                txtNamaPenerima.Text,
                txtNomorHP.Text,
                txtAlamatLengkap.Text,
                idKec,
                idKel,
                "Transfer Bank",   // metode pembayaran contoh
                totalHarga
            );

            if (idTransaksi <= 0)
            {
                MessageBox.Show("Gagal membuat transaksi!");
                return;
            }

            // -----------------------------
            // BUKA FORM PEMBAYARAN
            // -----------------------------
            FormCheckoutPembayaran f = new FormCheckoutPembayaran(
                _jasaDipilih,
                txtNamaPenerima.Text,
                txtNomorHP.Text,
                txtAlamatLengkap.Text,
                idKec,
                idKel,
                idTransaksi,
                _controller
            );

            f.Show();

        }
    }
}
