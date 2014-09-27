using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WYS.Helpers
{
    public static class TokenManager
    {

         public static String GetToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = new Guid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public static bool ValidateTokenTime(string token)
        {
            bool timeout = true;
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddHours(-1))
            {
                timeout = false;
            }
            return timeout;
        }
     
        
    }

    }
