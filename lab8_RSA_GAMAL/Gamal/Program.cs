using System;
using System.Linq;
using System.Numerics;

namespace Gamal
{
    internal class Program
    {
        private static void Main(string[] args)
        {
          
            Console.WriteLine("ВВЕДИЕТЕ СООБЩЕНИЕ:");
            string str = Console.ReadLine();

            Console.WriteLine("El-Gamal");
            DateTime start1 = DateTime.Now;
            string elgamalCrypted = EnCrypt(str);
            Console.WriteLine("ЗАШИФРОВАННОЕ СООБЩЕНИЕ = " + elgamalCrypted);
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Потраченное время на зашифровку: " + procTime1.TotalSeconds.ToString() + " sec");
            DateTime start2 = DateTime.Now;
            string elgamalDecrypted = DeCrypt(elgamalCrypted);
            Console.WriteLine("РАСШИФРОВАННОЕ СООБЩЕНИЕ = " + elgamalDecrypted);
            TimeSpan procTime2 = DateTime.Now - start2;
            Console.WriteLine("Потраченное время на расшифровку: " + procTime2.TotalSeconds.ToString() + " sec");

            Console.ReadLine();
        }

        private static BigInteger Power(BigInteger a, int b, int n) //Y
        { // a^b mod n
            BigInteger tmp = BigInteger.Pow(a, b);

            return tmp % n;
        }

        public static string EnCrypt(string str)
        {
            return Crypt(547, 91, 14, str); //p, q, x
        }

        public static string DeCrypt(string str)
        {
            return Decrypt(547, 14, str); //p, x
        }


       
        private static string Crypt(int p, int g, int x, string inString)
        {
            string result = "";
            BigInteger y = Power(g, x, p);//y=21
            Random rand = new Random();
            Console.WriteLine($"Public key (p,g,y)=({p},{g},{y})");
            Console.WriteLine($"Primary key x={x}");

            Console.Write("SHift text: ");
            int k = rand.Next() % (p - 2) + 1; // 1 < k < (p-1) 
            BigInteger a = Power(g, k, p); //посчитали ai=q^k mod p (273)
            foreach (int code in inString)//передаем строку, считываем каждую букву и берем ее код в аски
            {
                if (code > 0)
                {
                    Console.Write((char)code);

                    BigInteger b = Power((int)Power(y, k, p) * code, 1, p); //считаем bi= (y^k*mi) mod p =544( для N)
                    result += a + " " + b + " ";
                }
            }

            Console.WriteLine();
            return result;
        }

        private static string Decrypt(int p, int x, string inText)
        {
            string result = "";
           // Console.WriteLine("De shift text: ");

            string[] arr = inText.Split(' ').Where(xx => xx != "").ToArray();
            for (int i = 0; i < arr.Length; i += 2)
            {
                int a = int.Parse(arr[i]);
                int b = int.Parse(arr[i + 1]);

                if (a != 0 && b != 0)
                {
                    BigInteger deM = Power(b * BigInteger.Pow(a, p - 1 - x), 1, p);//mi= (bi*(ai)^x)^-1) mod p
                    char m = (char)deM;
                    result += m;
                }
            }

            return result;
        }
    }
}
