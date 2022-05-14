using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesktop
{
    public static class TokenSTC
    {
        public static string Token { get; set; }

        public static DateTime Expiration { get; set; }

        public static bool ExpirationToken(DateTime fecha)
        {
            if (DateTime.Now >= fecha)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
