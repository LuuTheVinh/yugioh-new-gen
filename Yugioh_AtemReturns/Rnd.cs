using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Yugioh_AtemReturns
{
    class Rnd
    {
        private static RNGCryptoServiceProvider rngCspRand;

        public Rnd()
        {
            rngCspRand = new RNGCryptoServiceProvider();
        }

        public static int Rand(int min, int max)
        {
            if (min == max)
                return min;
            if (rngCspRand == null)
                rngCspRand = new RNGCryptoServiceProvider();
            if ((max - min + 1) < byte.MaxValue)
            {
                byte[] randTemp = new byte[1];
                do
                {
                    rngCspRand.GetBytes(randTemp);
                }
                while (isFail(randTemp[0], (byte)max));
                return (randTemp[0] % (max - min + 1) + min);
            }
            else
            {
                int counter = 0;
                int temp = max / byte.MaxValue;

                byte[] randTemp = new byte[temp];

                rngCspRand.GetBytes(randTemp);

                for (int i = 0; i < temp; i++)
                {
                   counter += randTemp[i];
                }
                counter += Rand(0, max % byte.MaxValue);
                return counter;
            }
        }

        private static bool isFail(int number, int max)
        {
            int setValue = byte.MaxValue / max;
            return (number > setValue * max);
        }

    }
}
