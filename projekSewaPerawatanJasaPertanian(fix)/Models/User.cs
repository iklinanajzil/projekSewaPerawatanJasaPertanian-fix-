using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{
    public class UserModel
    {
        public int IdPengguna { get; set; } // GANTI UserId menjadi IdPengguna
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        admin, pelanggan
    }
}
