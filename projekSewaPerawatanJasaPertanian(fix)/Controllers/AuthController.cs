using Npgsql;
using projekSewaPerawatanJasaPertanian_fix_.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetEnv;
using projekSewaPerawatanJasaPertanian_fix_.Helpers;
using projekSewaPerawatanJasaPertanian_fix_.Models;

namespace projekSewaPerawatanJasaPertanian_fix_.Controllers
{
    public class AuthController
    {
        private DbContext _dbContext;

        public AuthController()
        {
            _dbContext = new DbContext();
        }

        public UserModel Login(UserModel user)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_dbContext.connStr))
                {
                    conn.Open();
                    string query = @"
                        SELECT id_pengguna, username, email, password, role FROM dataakun WHERE username = @username AND password = @password LIMIT 1";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        string hashedPassword = PasswordHelper.HashPassword(user.Password);

                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        using (var read = cmd.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                int userId = read.GetInt32(0);
                                string username = read.GetString(1);
                                string email = read.GetString(2);
                                string password = read.GetString(3);
                                string role = read.GetString(4);
                                UserRole roleEnum = (UserRole)Enum.Parse(typeof(UserRole), role);

                                UserModel loggedInuser = new UserModel
                                {
                                    UserId = userId,
                                    Username = username,
                                    Email = email,
                                    Password = password,
                                    Role = roleEnum
                                };

                                return loggedInuser;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LOGIN ERROR: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool Register(UserModel user)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_dbContext.connStr))
                {
                    conn.Open();
                    string query = @"
                         INSERT INTO dataakun (username, email, password, role)
                         VALUES(@username, @email, @password, @role::role_status);";

                    string hashPassword = PasswordHelper.HashPassword(user.Password);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", hashPassword);
                        cmd.Parameters.AddWithValue("@role", user.Role.ToString());

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Register ERROR: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
