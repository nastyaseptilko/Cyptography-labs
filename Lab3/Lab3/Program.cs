using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_SmallestCommonFactor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите числа для подсчета НОД:");
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            Console.WriteLine($"НОД({x},{y}) = {SmallestCommonFactor(x, y)}");
            Console.WriteLine();

            Console.WriteLine("Введите число для проверки является ли оно простым:");
            int s = int.Parse(Console.ReadLine());
            Console.WriteLine($"Является ли число {s} простым? Это {IsSimple(s)}.");
            Console.WriteLine();

            Console.WriteLine("Введите начало интервала:");
            int m = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите интервал:");
            int n = int.Parse(Console.ReadLine());

            SimplesInInterval(m, n);
            Console.WriteLine();

            Console.WriteLine("Введите номер для нахождения обратного по модулю:");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите модуль");

            int module = int.Parse(Console.ReadLine());

            if (SmallestCommonFactor(number, module) == 1)
            {
                ReverseElementByMod(number, module);
            }
            else
            {
                Console.WriteLine("НОД != 1");
            }
        }

        private static void ReverseElementByMod(int a, int m)
        {
            a = a % m;

            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    Console.WriteLine($"Обратный элемент {a} и модуль {m} это  {x}");
                }
            }
        }

        static int SmallestCommonFactor(int x, int y)
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                    x -= y;

                else
                    y -= x;
            }

            return Math.Max(x, y);
        }

        static bool IsSimple(int x)
        {
            if (x == 1)
                return true;

            else
            {
                for (int i = 2; i * i <= x; i++)
                {
                    if (x % i == 0)
                        return false;
                }
            }

            return true;
        }

        static void SimplesInInterval(int m, int n)
        {
            int counter = 0;
            if (n < m)
            {
                Console.WriteLine("Неверные значения!");
            }

            Console.Write($"Простые числа в интервале [{m},{n}]: ");

            for (int i = m; i <= n; i++)
            {
                if (IsSimple(i))
                {
                    Console.Write(i.ToString() + " ");
                    counter++;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Количество простых чисел {counter}");
        }
    }
}

