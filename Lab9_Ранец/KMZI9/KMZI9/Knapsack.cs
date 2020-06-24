using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZI9
{
    public class Knapsack
    {
        public string getcipher(string publickey, string data,int z) //для зашифрования
        {
            string data_result = "";
            string[] vals = publickey.Split(',');
            int[] weights = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++) weights[i] = Convert.ToInt32(vals[i]);
            int ptr = 0;
            int bit = 0;
            int total = 0;
            do
            {
                total = 0;
                for (int i = 0; i < z; i++)
                {
                    if (data[ptr] == '1') bit = 1;
                    else bit = 0;
                    total += weights[i] * bit;
                    ptr++;
                }
                if (data_result == "") data_result += total.ToString();
                else data_result += "," + total.ToString();

            } while (ptr < data.Length);


            return (data_result);

        }
        public int solve(int n, int m) //для расшифровки нахождения а^-1
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((n * i) % m) == 1) return (i);
            }
            return (res);
        }
        public string getknap(string key, string n, string m) //для расшифрования S
        {
            string[] vals = key.Split(',');
            string k = "";
            foreach (string v in vals)
            {
                int i = (Convert.ToInt32(v) * Convert.ToInt32(n)) % Convert.ToInt32(m);
                if (k == "") k += i.ToString();
                else k += "," + i.ToString();

            }
            return (k);

        }
        public string decipher(string key, int s,int z) //расшифрование
        {
            string res = "";
            string[] k = key.Split(',');
            int r = s;
            for(int i=z; i>0;i--)
            {
                if(r>=int.Parse(k[i-1]))
                {
                    res += '1';
                    r = r - int.Parse(k[i-1]);
                }
                else
                {
                    res += '0';
                }
            }
            return res;
        }
    }
}
