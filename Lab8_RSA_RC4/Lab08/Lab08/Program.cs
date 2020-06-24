using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;

namespace Lab08
{
    class Program
    {
        public static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                       'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                       'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                       'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                       '8', '9', '0' };
        static void Main(string[] args)
        {
            Process.Start("in.txt");
            Console.Write("Input p: ");
            int p = int.Parse(Console.ReadLine());
            Console.Write("Input q: ");
            int q = int.Parse(Console.ReadLine());

            var timer = new Stopwatch();
            timer.Start();
            Encrypt(p, q, out long d, out long e, out long n);
            timer.Stop();

            Console.WriteLine($"Encrypted {(float)timer.ElapsedMilliseconds / 1000} sec");
            Process.Start("enc.txt");
            timer.Reset();

            timer.Start();
            Decrypt(d, n);
            timer.Stop();

            Console.WriteLine($"Decrypted {(float)timer.ElapsedMilliseconds / 1000} sec");
            Process.Start("dec.txt");

            //RC4 task2

            byte[] key = { 12, 13, 90, 91, 240 };

            DateTime start1 = DateTime.Now;
            RC4 encoder = new RC4(key);
            string testString = "Septilko Anastasiya";
            Console.WriteLine("Test string: " + testString);
            byte[] testBytes = ASCIIEncoding.ASCII.GetBytes(testString);
            byte[] result = encoder.Encode(testBytes, testBytes.Length);

            string hexresult = "";
            foreach (byte bt in result)
            {
                hexresult += Convert.ToString(bt, 16);

            }
            Console.WriteLine("Зашифровали текст: " + hexresult);
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Потраченное время на зашифровку: " + procTime1.TotalSeconds.ToString() + " sec");

            DateTime start2 = DateTime.Now;
            RC4 decoder = new RC4(key);
            byte[] decryptedBytes = decoder.Decode(result, result.Length);
            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);
            Console.WriteLine("Расшифровали текст: " + decryptedString);
            TimeSpan procTime2 = DateTime.Now - start2;
            Console.WriteLine("Потраченное время на расшифровку: " + procTime2.TotalSeconds.ToString() + " sec");
        }

        private static void Encrypt(int p, int q, out long d, out long e, out long n)
        {
            string text = "";
            if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
            {
                n = p * q;
                long m = (p - 1) * (q - 1); // вычисляем функцию Эйлера
                e = Calculate_e(m); //179
                d = Calculate_d(e, m); //179

                using (var sr = new StreamReader("in.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        text += sr.ReadLine();
                    }
                }

                text = text.ToUpper();
                var result = RSA_Encode(text, e, n);

                using (var sw = new StreamWriter("enc.txt"))
                {
                    foreach (string item in result)
                        sw.WriteLine(item);
                }
            }
            else
                throw new Exception("p or q are not prime numbers!");
        }

        //расшифровать
        private static void Decrypt(long d, long n)
        {
            try
            {
                var input = new List<string>();
                using (var sr = new StreamReader("enc.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        input.Add(sr.ReadLine());
                    }
                }

                string result = RSA_Decode(input, d, n);

                using (var sw = new StreamWriter("dec.txt"))
                {
                    sw.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        private static List<string> RSA_Encode(string text, long e, long n)
        {
            var result = new List<string>();
            BigInteger bi;

            for (int i = 0; i < text.Length; i++)
            {
                int index = Array.IndexOf(characters, text[i]);
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);
                var n_ = new BigInteger((int)n);
                bi %= n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        //расшифровать
        private static string RSA_Decode(List<string> input, long d, long n)
        {
            string result = "";
            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);
                var n_ = new BigInteger((int)n);
                bi %= n_;
                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        //вычисление параметра e. e должно быть взаимно простым с m и меньше его
        private static long Calculate_e(long m)
        {
            long e = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (e % i == 0)) //если имеют общие делители
                {
                    e--;
                    i = 1;
                }

            return e;
        }

        //вычисление параметра d - обратное к числу e
        private static long Calculate_d(long e, long m)
        {
            long d = 10;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    d++;
            }

            return d;
        }
    }
}
