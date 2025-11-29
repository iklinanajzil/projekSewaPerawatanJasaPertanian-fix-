
using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Database;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.Data;

using System.Windows.Forms;


namespace projekSewaPerawatanJasaPertanian_fix_.Controllers
{
    public class DataServiceController
    {
        private readonly DbContext _dbContext;
        public DataServiceController()
        {
            _dbContext = new DbContext();
        }
        public List<JasaModel> GetJasaTersedia()
        {
            List<JasaModel> jasaList = new List<JasaModel>();

            string query = @"
                SELECT 
                    DJ.id_jasa, DJ.nama_jasa, DJ.harga_jasa, DJ.simbol, DJ.deskripsi_jasa,
                    JJ.id_jadwal, JJ.hari, JJ.tanggal_tersedia, 
                    JJ.jam_mulai, JJ.jam_akhir, JJ.slot_ketersediaan 
                FROM 
                    DataJasa DJ
                INNER JOIN 
                    JadwalJasa JJ ON DJ.id_jasa = JJ.id_jasa
                WHERE
                    JJ.slot_ketersediaan > 0
                ORDER BY
                    DJ.id_jasa, JJ.tanggal_tersedia, JJ.jam_mulai;";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                jasaList.Add(new JasaModel
                                {
                                    IdJasa = reader.GetInt32(reader.GetOrdinal("id_jasa")),
                                    IdJadwal = reader.GetInt32(reader.GetOrdinal("id_jadwal")),
                                    NamaJasa = reader.GetString(reader.GetOrdinal("nama_jasa")),
                                    HargaJasa = reader.GetDecimal(reader.GetOrdinal("harga_jasa")),
                                    HariTersedia = reader.GetString(reader.GetOrdinal("hari")),
                                    TanggalJadwal = reader.GetDateTime(reader.GetOrdinal("tanggal_tersedia")),
                                    JamMulai = (TimeSpan)reader.GetValue(reader.GetOrdinal("jam_mulai")),
                                    JamAkhir = (TimeSpan)reader.GetValue(reader.GetOrdinal("jam_akhir")),
                                    SlotKetersediaan = reader.GetInt32(reader.GetOrdinal("slot_ketersediaan")).ToString(),
                                    DeskripsiJasa = reader.GetString(reader.GetOrdinal("deskripsi_jasa")),
                                    SimbolJasa = reader.GetString(reader.GetOrdinal("simbol"))
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database Error saat memuat data jasa: {ex.Message}");
                }
            }
            return jasaList;
        }
        public void DeleteJasa(int idJasa)
        {
            string deleteJadwalQuery = @"DELETE FROM JadwalJasa WHERE id_jasa = @idjasa;";
            string deleteHeaderQuery = @"DELETE FROM DataJasa WHERE id_jasa = @idjasa;";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                NpgsqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (NpgsqlCommand cmdJadwal = new NpgsqlCommand(deleteJadwalQuery, conn, transaction))
                    {
                        cmdJadwal.Parameters.AddWithValue("@idjasa", idJasa);
                        cmdJadwal.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand cmdHeader = new NpgsqlCommand(deleteHeaderQuery, conn, transaction))
                    {
                        cmdHeader.Parameters.AddWithValue("@idjasa", idJasa);
                        cmdHeader.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (NpgsqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Gagal menghapus jasa. Detail: {ex.Message}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Terjadi kesalahan umum saat menghapus jasa: {ex.Message}");
                }
            }
        }

        public void InsertJasaHeaderOnly(string namaJasa, string deskripsi, string simbol, decimal harga)
        {
            string insertQuery = @"
                INSERT INTO DataJasa (nama_jasa, deskripsi_jasa, simbol, harga_jasa)
                VALUES (@nama, @deskripsi, @simbol, @harga)";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", namaJasa);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@simbol", simbol);
                        cmd.Parameters.AddWithValue("@harga", harga);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception($"Gagal menambah layanan jasa: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Terjadi kesalahan umum saat insert jasa: {ex.Message}");
                }
            }
        }

        public DataTable GetAllDataJasa()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_jasa, nama_jasa FROM DataJasa ORDER BY nama_jasa ASC";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void InsertJadwalBaru(int idJasa, string hari, DateTime tanggal, TimeSpan jamMulai, TimeSpan jamAkhir, int slot)
        {
            string insertQuery = @"
                INSERT INTO JadwalJasa (id_jasa, hari, tanggal_tersedia, jam_mulai, jam_akhir, slot_ketersediaan)
                VALUES (@idjasa, @hari, @tanggal, @jammulai, @jamakhir, @slot)";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idjasa", idJasa);
                        cmd.Parameters.AddWithValue("@hari", hari);
                        cmd.Parameters.AddWithValue("@tanggal", tanggal.Date);
                        cmd.Parameters.AddWithValue("@jammulai", jamMulai);
                        cmd.Parameters.AddWithValue("@jamakhir", jamAkhir);
                        cmd.Parameters.AddWithValue("@slot", slot);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception($"Gagal menambah jadwal baru: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Terjadi kesalahan umum saat insert jadwal: {ex.Message}");
                }
            }
        }

        public void UpdateJasaHeader(int idJasa, string namaJasa, string deskripsi, string simbol, decimal harga)
        {
            string updateQuery = @"
                UPDATE DataJasa 
                SET 
                    nama_jasa = @nama, 
                    deskripsi_jasa = @deskripsi, 
                    simbol = @simbol, 
                    harga_jasa = @harga
                WHERE 
                    id_jasa = @idjasa;";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idjasa", idJasa);
                        cmd.Parameters.AddWithValue("@nama", namaJasa);
                        cmd.Parameters.AddWithValue("@deskripsi", deskripsi);
                        cmd.Parameters.AddWithValue("@simbol", simbol);
                        cmd.Parameters.AddWithValue("@harga", harga);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception($"Gagal memperbarui header jasa di database. Detail: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Terjadi kesalahan umum saat update jasa header: {ex.Message}");
                }
            }
        }

        // --- METHOD 7: UpdateJadwal ---
        public void UpdateJadwal(int idJadwal, int idJasa, string hari, DateTime tanggal, TimeSpan jamMulai, TimeSpan jamAkhir, int slot)
        {
            string updateQuery = @"
                UPDATE JadwalJasa 
                SET 
                    id_jasa = @idjasa, 
                    hari = @hari, 
                    tanggal_tersedia = @tanggal, 
                    jam_mulai = @jammulai, 
                    jam_akhir = @jamakhir,
                    slot_ketersediaan = @slot
                WHERE 
                    id_jadwal = @idjadwal;";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idjadwal", idJadwal);
                        cmd.Parameters.AddWithValue("@idjasa", idJasa);
                        cmd.Parameters.AddWithValue("@hari", hari);
                        cmd.Parameters.AddWithValue("@tanggal", tanggal.Date);
                        cmd.Parameters.AddWithValue("@jammulai", jamMulai);
                        cmd.Parameters.AddWithValue("@jamakhir", jamAkhir);
                        cmd.Parameters.AddWithValue("@slot", slot);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception($"Gagal memperbarui jadwal jasa di database. Detail: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Terjadi kesalahan umum saat update jadwal: {ex.Message}");
                }
            }
        }

        // --- METHOD 8: DeleteJadwal ---
        public void DeleteJadwal(int idJadwal)
        {
            string deleteQuery = "DELETE FROM JadwalJasa WHERE id_jadwal = @idJadwal";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@idJadwal", idJadwal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- METHOD 9: GetAllDataJasaWithJadwal ---
        public List<JasaModel> GetAllDataJasaWithJadwal()
        {
            List<JasaModel> jasaList = new List<JasaModel>();

            string query = @"
                SELECT 
                    DJ.id_jasa, DJ.nama_jasa, DJ.harga_jasa, DJ.deskripsi_jasa, DJ.simbol,
                    JJ.id_jadwal, JJ.hari, JJ.tanggal_tersedia, JJ.jam_mulai, JJ.jam_akhir, JJ.slot_ketersediaan
                FROM 
                    DataJasa DJ
                INNER JOIN 
                    JadwalJasa JJ ON DJ.id_jasa = JJ.id_jasa
                ORDER BY 
                    DJ.id_jasa, JJ.tanggal_tersedia, JJ.jam_mulai;";

            using (NpgsqlConnection conn = _dbContext.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JasaModel jasa = new JasaModel
                                {
                                    IdJasa = reader.GetInt32(reader.GetOrdinal("id_jasa")),
                                    NamaJasa = reader.GetString(reader.GetOrdinal("nama_jasa")),
                                    HargaJasa = reader.GetDecimal(reader.GetOrdinal("harga_jasa")),
                                    DeskripsiJasa = reader.GetString(reader.GetOrdinal("deskripsi_jasa")),
                                    SimbolJasa = reader.GetString(reader.GetOrdinal("simbol")),

                                    IdJadwal = reader.GetInt32(reader.GetOrdinal("id_jadwal")),
                                    TanggalJadwal = reader.GetDateTime(reader.GetOrdinal("tanggal_tersedia")),
                                    JamMulai = (TimeSpan)reader.GetValue(reader.GetOrdinal("jam_mulai")),
                                    JamAkhir = (TimeSpan)reader.GetValue(reader.GetOrdinal("jam_akhir")),
                                    SlotKetersediaan = reader.GetInt32(reader.GetOrdinal("slot_ketersediaan")).ToString()
                                };
                                jasaList.Add(jasa);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database Error saat memuat data jasa: {ex.Message}");
                }
            }
            return jasaList;
        }

        public int InsertPemesananLengkap(CheckoutModel checkout, List<JasaModel> jasaList, string buktiPembayaran)
        {
            int idTransaksiBaru = 0;

            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // ----- Insert transaksi -----
                        string queryTrans =
                            @"INSERT INTO transaksi 
                                (id_pengguna, nama_penerima, nomor_hp, alamat_lengkap, id_kecamatan, id_kelurahan, 
                                 metode_pembayaran, status, total_harga, bukti_pembayaran)
                              VALUES 
                                (@id_pengguna, @nama, @hp, @alamat, @kec, @kel, @metode, @status, @total, @bukti)
                              RETURNING id_transaksi";

                        using (var cmd = new NpgsqlCommand(queryTrans, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_pengguna", checkout.IdPengguna);
                            cmd.Parameters.AddWithValue("@nama", checkout.NamaPenerima);
                            cmd.Parameters.AddWithValue("@hp", checkout.NomorHP);
                            cmd.Parameters.AddWithValue("@alamat", checkout.AlamatLengkap);
                            cmd.Parameters.AddWithValue("@kec", checkout.IdKecamatan);
                            cmd.Parameters.AddWithValue("@kel", checkout.IdKelurahan);
                            cmd.Parameters.AddWithValue("@metode", checkout.MetodePembayaran);
                            cmd.Parameters.AddWithValue("@status", checkout.Status);
                            cmd.Parameters.AddWithValue("@total", checkout.TotalHarga);

                            if (buktiPembayaran == null)
                                cmd.Parameters.AddWithValue("@bukti", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@bukti", buktiPembayaran);

                            idTransaksiBaru = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // ----- Insert detail transaksi -----
                        string queryDetail =
                            @"INSERT INTO detail_transaksi (id_transaksi, id_jasa, nama_jasa, harga)
                              VALUES (@id_transaksi, @id_jasa, @nama_jasa, @harga)";

                        foreach (var item in jasaList)
                        {
                            using (var cmd = new NpgsqlCommand(queryDetail, conn))
                            {
                                cmd.Parameters.AddWithValue("@id_transaksi", idTransaksiBaru);
                                cmd.Parameters.AddWithValue("@id_jasa", item.IdJasa);
                                cmd.Parameters.AddWithValue("@nama_jasa", item.NamaJasa);
                                cmd.Parameters.AddWithValue("@harga", item.HargaJasa);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }

            return idTransaksiBaru;
        }
        public DataTable GetKecamatan()
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();

                string q = "SELECT id_kecamatan, nama_kecamatan FROM kecamatan";
                using (var da = new NpgsqlDataAdapter(q, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetKelurahan(int idKecamatan)
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();

                string q = "SELECT id_kelurahan, nama_kelurahan FROM kelurahan WHERE id_kecamatan=@id";
                using (var cmd = new NpgsqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKecamatan);

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        public bool UpdateBuktiPembayaran(int idTransaksi, byte[] bukti)
        {
            string query = @"UPDATE transaksi 
                     SET bukti_pembayaran = @bukti
                     WHERE id_transaksi = @id";

            using (var conn = _dbContext.GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bukti", bukti);
                cmd.Parameters.AddWithValue("@id", idTransaksi);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public void KurangiSlotByTransaksi(int idTransaksi)
        {
            string query = @"
        UPDATE jadwaljasa j
        SET slot_ketersediaan = slot_ketersediaan - 1
        FROM detailtransaksi d
        WHERE d.id_transaksi = @id
        AND d.id_jasa = j.id_jasa";

            using (var conn = _dbContext.GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", idTransaksi);
                cmd.ExecuteNonQuery();
            }
        }

        public int InsertTransaksi(
    int idPelanggan,
    string namaPenerima,
    string nomorHP,
    string alamatLengkap,
    int idKecamatan,
    int idKelurahan,
    string metodePembayaran,
    decimal totalBayar)
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();

                string query = @"
            INSERT INTO transaksi
            (id_pengguna, nama_penerima, no_hp, alamat_lengkap, 
             id_kelurahan, id_kecamatan, tanggal_transaksi, 
             metode_pembayaran, status, total_bayar)
            VALUES
            (@id, @nama, @hp, @alamat, @kel, @kec, NOW(),
             @metode, 'Menunggu Bukti', @total)
            RETURNING id_transaksi;
        ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPelanggan);
                    cmd.Parameters.AddWithValue("@nama", namaPenerima);
                    cmd.Parameters.AddWithValue("@hp", nomorHP);
                    cmd.Parameters.AddWithValue("@alamat", alamatLengkap);
                    cmd.Parameters.AddWithValue("@kel", idKelurahan);
                    cmd.Parameters.AddWithValue("@kec", idKecamatan);
                    cmd.Parameters.AddWithValue("@metode", metodePembayaran);
                    cmd.Parameters.AddWithValue("@total", totalBayar);

                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newId;
                }
            }
        }
        public void InsertDetailTransaksi(int idTransaksi, JasaModel jasa)
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();

                string query = @"
            INSERT INTO detailtransaksi
            (id_transaksi, id_jasa, jumlah_pesanan, subtotal_harga)
            VALUES
            (@id_trans, @id_jasa, 1, @harga);
        ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_trans", idTransaksi);
                    cmd.Parameters.AddWithValue("@id_jasa", jasa.IdJasa);
                    cmd.Parameters.AddWithValue("@harga", jasa.HargaJasa);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

    

    


