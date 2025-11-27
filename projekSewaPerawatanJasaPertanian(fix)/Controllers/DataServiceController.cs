using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Database;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Controllers
{
    public class DataServiceController
    {
        public List<JasaModel> GetJasaTersedia()
        {
            List<JasaModel> jasaList = new List<JasaModel>();
            DbContext dbContext = new DbContext();

            // Query PostgreSQL untuk menggabungkan DataJasa dan JadwalJasa
            string query = @"
            SELECT 
                DJ.nama_jasa, 
                DJ.harga_jasa, 
                DJ.deskripsi_jasa, 
                DJ.simbol,
                JJ.hari, 
                JJ.tanggal_tersedia, 
                TO_CHAR(JJ.jam_mulai, 'HH24:MI') || ' - ' || TO_CHAR(JJ.jam_akhir, 'HH24:MI') AS jam_tersedia,
                CASE WHEN JJ.slot_ketersediaan > 0 THEN 'Tersedia' ELSE 'Penuh' END AS status_ketersediaan,
                JJ.id_jadwal
            FROM DataJasa DJ
            JOIN JadwalJasa JJ ON DJ.id_jasa = JJ.id_jasa
            WHERE JJ.slot_ketersediaan > 0 AND JJ.tanggal_tersedia >= CURRENT_DATE
            ORDER BY JJ.tanggal_tersedia ASC, JJ.jam_mulai ASC;";

            using (NpgsqlConnection conn = dbContext.GetConnection())
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
                                IdJadwal = reader.GetInt32(reader.GetOrdinal("id_jadwal")),
                                NamaJasa = reader.GetString(reader.GetOrdinal("nama_jasa")),
                                HargaJasa = reader.GetDecimal(reader.GetOrdinal("harga_jasa")),
                                HariTersedia = reader.GetString(reader.GetOrdinal("hari")),
                                TanggalTersedia = reader.GetDateTime(reader.GetOrdinal("tanggal_tersedia")),
                                JamTersedia = reader.GetString(reader.GetOrdinal("jam_tersedia")),
                                StatusKetersediaan = reader.GetString(reader.GetOrdinal("status_ketersediaan")),
                                DeskripsiJasa = reader.GetString(reader.GetOrdinal("deskripsi_jasa")),
                                SimbolJasa = reader.GetString(reader.GetOrdinal("simbol"))
                            });
                        }
                    }
                }
            }
            return jasaList;
        }
        // --- Metode 2: Menyimpan Transaksi Baru (Checkout) ---
        public void InsertTransaksi(int idPengguna, List<int> idJadwalTerpilih, string namaPenerima, string noHp, string alamatLengkap, int idKelurahan, int idKecamatan, string metodePembayaran)
        {
            DbContext dbContext = new DbContext();
            using (NpgsqlConnection conn = dbContext.GetConnection())
            {
                conn.Open();

                // Menggunakan NpgsqlTransaction untuk memastikan semua operasi berhasil
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        decimal totalBayar = 0;

                        var idJadwalsParam = new NpgsqlParameter("@idJadwals", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer);
                        idJadwalsParam.Value = idJadwalTerpilih.ToArray();

                        List<(int idJasa, decimal hargaJasa)> detailItems = new List<(int, decimal)>();

                        // 1. Ambil ID Jasa dan Harga untuk item yang dipilih
                        string getTotalQuery = @"
                        SELECT DJ.id_jasa, DJ.harga_jasa
                        FROM JadwalJasa JJ
                        JOIN DataJasa DJ ON JJ.id_jasa = DJ.id_jasa
                        WHERE JJ.id_jadwal = ANY(@idJadwals)";

                        using (NpgsqlCommand cmdTotal = new NpgsqlCommand(getTotalQuery, conn, transaction))
                        {
                            cmdTotal.Parameters.Add(idJadwalsParam);
                            using (var reader = cmdTotal.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    detailItems.Add((reader.GetInt32(reader.GetOrdinal("id_jasa")), reader.GetDecimal(reader.GetOrdinal("harga_jasa"))));
                                    totalBayar += reader.GetDecimal(reader.GetOrdinal("harga_jasa"));
                                }
                            }
                        }

                        if (detailItems.Count == 0) throw new Exception("Tidak ada jasa yang ditemukan untuk diproses.");

                        // 2. Masukkan Transaksi (Header)
                        string insertTransaksiQuery = @"
                        INSERT INTO Transaksi (id_pengguna, nama_penerima, no_hp, alamat_lengkap, id_kelurahan, id_kecamatan, metode_pembayaran, status, total_bayar)
                        VALUES (@idPengguna, @namaPenerima, @noHp, @alamatLengkap, @idKelurahan, @idKecamatan, @metodePembayaran, 'menunggu_pembayaran', @totalBayar)
                        RETURNING id_transaksi;";

                        int idTransaksiBaru;
                        using (NpgsqlCommand cmdTrans = new NpgsqlCommand(insertTransaksiQuery, conn, transaction))
                        {
                            cmdTrans.Parameters.AddWithValue("@idPengguna", idPengguna);
                            cmdTrans.Parameters.AddWithValue("@namaPenerima", namaPenerima);
                            cmdTrans.Parameters.AddWithValue("@noHp", noHp);
                            cmdTrans.Parameters.AddWithValue("@alamatLengkap", alamatLengkap);
                            cmdTrans.Parameters.AddWithValue("@idKelurahan", idKelurahan);
                            cmdTrans.Parameters.AddWithValue("@idKecamatan", idKecamatan);
                            cmdTrans.Parameters.AddWithValue("@metodePembayaran", metodePembayaran);
                            cmdTrans.Parameters.AddWithValue("@totalBayar", totalBayar);

                            idTransaksiBaru = (int)cmdTrans.ExecuteScalar();
                        }

                        // 3. Masukkan DetailTransaksi
                        foreach (var item in detailItems)
                        {
                            string insertDetailQuery = @"
                            INSERT INTO DetailTransaksi (id_transaksi, id_jasa, jumlah_pesanan, subtotal_harga)
                            VALUES (@idTransaksi, @idJasa, 1, @subtotalHarga);";

                            using (NpgsqlCommand cmdDetail = new NpgsqlCommand(insertDetailQuery, conn, transaction))
                            {
                                cmdDetail.Parameters.AddWithValue("@idTransaksi", idTransaksiBaru);
                                cmdDetail.Parameters.AddWithValue("@idJasa", item.idJasa);
                                cmdDetail.Parameters.AddWithValue("@subtotalHarga", item.hargaJasa);
                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // 4. Kurangi Slot Ketersediaan (PENTING!)
                        string updateSlotQuery = @"
                        UPDATE JadwalJasa 
                        SET slot_ketersediaan = slot_ketersediaan - 1
                        WHERE id_jadwal = ANY(@idJadwals)";

                        using (NpgsqlCommand cmdSlot = new NpgsqlCommand(updateSlotQuery, conn, transaction))
                        {
                            cmdSlot.Parameters.Add(idJadwalsParam);
                            cmdSlot.ExecuteNonQuery();
                        }

                        // COMMIT jika semua berhasil
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // ROLLBACK jika ada kegagalan
                        transaction.Rollback();
                        throw new Exception("Gagal membuat transaksi: " + ex.Message);
                    }
                }
            }
        }
        public DataTable GetAllKecamatan()
        {
            DbContext dbContext = new DbContext();
            DataTable dt = new DataTable();
            string query = "SELECT id_kecamatan, nama_kecamatan FROM Kecamatan ORDER BY nama_kecamatan ASC";

            using (NpgsqlConnection conn = dbContext.GetConnection())
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

        // --- Metode 4: Mengambil Kelurahan berdasarkan ID Kecamatan ---
        public DataTable GetKelurahanByKecamatan(int idKecamatan)
        {
            DbContext dbContext = new DbContext();
            DataTable dt = new DataTable();
            string query = "SELECT id_kelurahan, nama_kelurahan FROM Kelurahan WHERE id_kecamatan = @idKecamatan ORDER BY nama_kelurahan ASC";

            using (NpgsqlConnection conn = dbContext.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKecamatan", idKecamatan);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}

