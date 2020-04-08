using System;
using System.Text;

namespace DirectorServer
{
    public class TokenGenerator
    {
        private const string UPPERPOOL = "QWERTYUIOPASDFGHJKLZXCVBNM";
        private const string NUMBERPOOL = "0123456789";

        public static string generateToken(int lenght)
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lenght; i++)
            {
                if (i % 2 == 0)
                    sb.Append(UPPERPOOL[rand.Next(0, UPPERPOOL.Length)]);
                else
                    sb.Append(NUMBERPOOL[rand.Next(0, NUMBERPOOL.Length)]);
            }
            return sb.ToString();
        }
    }
}