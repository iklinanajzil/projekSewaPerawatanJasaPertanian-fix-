using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Database;

namespace projekSewaPerawatanJasaPertanian_fix_.Controllers
{
    public class UserDashboardController
    {
        private readonly DbContext _db;

        public UserDashboardController()
        {
            _db = new DbContext();
        }

        public (int total, int proses, int selesai, decimal pengeluaran) GetDashboardStats(int userId)
        {
            int total = 0;
            int proses = 0;
            int selesai = 0;
            decimal pengeluaran = 0;

            using (var conn = _db.GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM transaksi WHERE id_pengguna = @uid", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }

                using (var cmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM transaksi WHERE id_pengguna = @uid AND status = 'sedang berlangsung'", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    proses = Convert.ToInt32(cmd.ExecuteScalar());
                }

                using (var cmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM transaksi WHERE id_pengguna = @uid AND status = 'selesai'", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    selesai = Convert.ToInt32(cmd.ExecuteScalar());
                }

                using (var cmd = new NpgsqlCommand(
                    "SELECT COALESCE(SUM(total_bayar), 0) FROM transaksi WHERE id_pengguna = @uid", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    pengeluaran = Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }

            return (total, proses, selesai, pengeluaran);
        }
    }
}


