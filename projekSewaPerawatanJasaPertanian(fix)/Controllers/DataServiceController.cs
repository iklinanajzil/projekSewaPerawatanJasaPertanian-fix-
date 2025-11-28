using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Database;
using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
  
            string query = @"
                   SELECT 
                        DJ.id_jasa, DJ.nama_jasa, DJ.harga_jasa, DJ.simbol, DJ.deskripsi_jasa, -- Pastikan deskripsi_jasa lowercase
                        JJ.id_jadwal, JJ.hari, JJ.tanggal_tersedia, 
                        JJ.jam_mulai, JJ.jam_akhir, JJ.slot_ketersediaan -- Kolom Waktu dan Slot yang benar
                            FROM 
                                DataJasa DJ
                            INNER JOIN 
                                JadwalJasa JJ ON DJ.id_jasa = JJ.id_jasa
                            WHERE
                                JJ.slot_ketersediaan > 0";


            using(NpgsqlConnection conn = dbContext.GetConnection())
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

        public void DeleteJasa(int idJasa)
        {
            DbContext dbContext = new DbContext();

            string deleteJadwalQuery = @"DELETE FROM JadwalJasa WHERE id_jasa = @idjasa;";
            string deleteHeaderQuery = @"DELETE FROM DataJasa WHERE id_jasa = @idjasa;";
            using (NpgsqlConnection conn = dbContext.GetConnection())
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
            DbContext dbContext = new DbContext();         
            string insertQuery = @"
    INSERT INTO DataJasa (nama_jasa, deskripsi_jasa, simbol, harga_jasa)
    VALUES (@nama, @deskripsi, @simbol, @harga)";
            
            using (NpgsqlConnection conn = dbContext.GetConnection())
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
            DbContext dbContext = new DbContext();
            DataTable dt = new DataTable();
            string query = "SELECT id_jasa, nama_jasa FROM DataJasa ORDER BY nama_jasa ASC";

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


        public void InsertJadwalBaru(int idJasa, string hari, DateTime tanggal, TimeSpan jamMulai, TimeSpan jamAkhir, int slot)
        {
            DbContext dbContext = new DbContext();

            string insertQuery = @"
    INSERT INTO JadwalJasa (id_jasa, hari, tanggal_tersedia, jam_mulai, jam_akhir, slot_ketersediaan)
    VALUES (@idjasa, @hari, @tanggal, @jammulai, @jamakhir, @slot)";

            using (NpgsqlConnection conn = dbContext.GetConnection())
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
            DbContext dbContext = new DbContext();

            string updateQuery = @"
    UPDATE DataJasa 
    SET 
        nama_jasa = @nama, 
        deskripsi_jasa = @deskripsi, 
        simbol = @simbol, 
        harga_jasa = @harga
    WHERE 
        id_jasa = @idjasa;";

            using (NpgsqlConnection conn = dbContext.GetConnection())
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

        public void UpdateJadwal(int idJadwal, int idJasa, string hari, DateTime tanggal, TimeSpan jamMulai, TimeSpan jamAkhir, int slot)
        {
            DbContext dbContext = new DbContext();

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

            using (NpgsqlConnection conn = dbContext.GetConnection())
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


        public void DeleteJadwal(int idJadwal)
        {
            DbContext dbContext = new DbContext();
            string deleteQuery = "DELETE FROM JadwalJasa WHERE id_jadwal = @idJadwal";

            using (NpgsqlConnection conn = dbContext.GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@idJadwal", idJadwal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JasaModel> GetAllDataJasaWithJadwal()
        {
            List<JasaModel> jasaList = new List<JasaModel>();
            DbContext dbContext = new DbContext();

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

            using (NpgsqlConnection conn = dbContext.GetConnection())
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
                               
                            }
                            ;
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
    }
}

