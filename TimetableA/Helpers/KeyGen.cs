using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.API.Helpers
{
    public class KeyGen
    {
        public static string Generate(int length = 16)
        {
            var builder = new StringBuilder();
            var rnd = new Random();

            for (int i = 0; i < length; i++)
                builder.Append(GetGenChar(rnd));

            return builder.ToString();
        }

        private static char GetGenChar(Random rnd)
        {
            var output = (char)rnd.Next('0', 'z');

            if (char.IsLetterOrDigit(output))
                return output;

            return GetGenChar(rnd);
        }
    }
}
