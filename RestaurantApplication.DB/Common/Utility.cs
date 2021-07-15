using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Common
{
    public static class Utility
    {
        static Random random = new Random();
        public static int GetRandomNumber(int length)
        {
            var chars = "1234567890";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return Convert.ToInt32(result);
        }
    }
}
