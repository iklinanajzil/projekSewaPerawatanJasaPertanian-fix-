using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Models
{

    public static class SessionManager
    {
        // Properti statis yang menyimpan ID pengguna yang sedang login
        public static int CurrentUserId { get; private set; } = 0;

        // Properti statis yang menyimpan role pengguna
        public static string CurrentUserRole { get; private set; } = "";

        // Metode yang dipanggil saat user berhasil login
        public static void Login(int userId, string userRole)
        {
            CurrentUserId = userId;
            CurrentUserRole = userRole;
        }

        // Metode untuk mengakhiri sesi
        public static void Logout()
        {
            CurrentUserId = 0;
            CurrentUserRole = "";
        }
    }
}
