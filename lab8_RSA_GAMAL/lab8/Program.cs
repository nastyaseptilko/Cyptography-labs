using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace lab8
{
    class Program
    {

        static void Main(string[] args)
        {

            char[] characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                                                        'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                                                        'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };

            long p, q, n, fi, e, d;
            string message, decodedMes;
            List<string> encodedMes;

            Console.WriteLine("ВВЕДИТЕ P И Q (через интер):");
            p = Convert.ToInt32(Console.ReadLine());
            q = Convert.ToInt32(Console.ReadLine());
            if (IsPrimary(p) && IsPrimary(q))
            {
                n = p * q;
                fi = Eiler(p, q); 
                e = getE(fi);
                d = getD(e, fi);

                Console.WriteLine("ВВЕДИТЕ ТЕКСТ:");
                message = Convert.ToString(Console.ReadLine());
                message.ToUpper();

                DateTime start1 = DateTime.Now;
                encodedMes = Encode(message, e, n, characters);
                Console.WriteLine("ЗАШИФРОВАННОЕ СООБЩЕНИЕ: ");
                foreach (string item in encodedMes)
                    Console.Write(item + " ");
                Console.WriteLine();
                TimeSpan procTime1 = DateTime.Now - start1;
                Console.WriteLine("Потраченное время на зашифровку: " + procTime1.TotalSeconds.ToString() + " sec");

                DateTime start2 = DateTime.Now;
                decodedMes = Decode(encodedMes, d, n, characters);
                Console.WriteLine("РАСШИФРОВАННОЕ СООБЩЕНИЕ: " + decodedMes);
                TimeSpan procTime2 = DateTime.Now - start2;
                Console.WriteLine("Потраченное время на расшифровку: " + procTime2.TotalSeconds.ToString() + " sec");

            }
            else
            {
                Console.WriteLine(" p and q should be primary!");
                Console.ReadLine();
            }
        }

        public static bool IsPrimary(long a)
        {
            if (a < 2)
                return false;

            if (a == 2)
                return true;

            for (long i = 2; i < a; i++)
                if (a % i == 0)
                    return false;

            return true;
        }

        public static long Eiler(long a, long b) //функция Эйлера
        {
            return (a - 1) * (b - 1);
        }

        public static long getE(long fi) //1 < e < fi(n)
        {
            long e = fi - 1;

            for (long i = 2; i < fi; i++)
                if ((fi % i == 0) && (e % i == 0)) //если имеют общие делители
                {
                    e--;
                    i = 1;
                }

            return e;
        }

        public static long getD(long e, long fi)
        {
            long d = 10;

            while (true)
            {
                if ((e * d) % fi == 1)
                    break;
                else
                    d++;
            }

            return d;
        }

        public static List<string> Encode(string s, long e, long n, char[] alphabet)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(alphabet, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        public static string Decode(List<string> input, long d, long n, char[] alphabet)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += alphabet[index].ToString();
            }

            return result;
        }

    }
}
