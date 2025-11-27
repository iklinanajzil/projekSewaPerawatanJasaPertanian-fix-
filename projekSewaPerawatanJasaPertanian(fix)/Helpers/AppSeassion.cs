using projekSewaPerawatanJasaPertanian_fix_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Helpers
{
    public static class AppSession
    {
        public static UserModel CurrentUser { get; private set; }
        public static bool IsAuthenticated => CurrentUser != null;

        public static void SetUser(UserModel user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
