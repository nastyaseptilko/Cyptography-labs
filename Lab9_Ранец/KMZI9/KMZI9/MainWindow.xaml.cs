using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMZI9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string priv;
        string base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 +";
        Knapsack knapsack = new Knapsack();
        public MainWindow()
        {
            InitializeComponent();
        }
        public int[] Generate(int z)
        {
            Random rnd = new Random();
            int[] k = new int[z];
            int sum = 0;
            for(int i =0;i<z;i++)
            {
                k[i] = rnd.Next(sum, sum + 23);
                sum += k[i];
            }
            return k;
        }
        private void Button_Click(object sender, RoutedEventArgs e) ///шифруем
        {
            DateTime t = DateTime.Now;
            string n = A.Text;
            string m = Module.Text;
            StringBuilder sb = new StringBuilder();
            byte[] bytes= Encoding.ASCII.GetBytes(TextToEncrypt.Text);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (int.Parse(Z.Text) == 8)
                {
                    sb.Append(Convert.ToString(bytes[i], 2).PadLeft(8, '0'));
                }
            }
            if(int.Parse(Z.Text) == 6)
            {
                    foreach (char c in TextToEncrypt.Text)
                    {
                        int ind = base64.IndexOf(c);
                        if (ind == -1)
                        {
                            ind = 0;
                        }
                        byte s = Convert.ToByte(ind);
                        sb.Append(Convert.ToString(s, 2).PadLeft(6, '0'));
                    }
            }
            string get_public = knapsack.getknap(priv, n, m);
            string cipher = knapsack.getcipher(get_public, sb.ToString(), int.Parse(Z.Text));
            TextToDecrypt.Text = cipher.ToString();
            Time.Text = (DateTime.Now - t).ToString();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //генерируем последовательность
        {
            priv = "";
            int[] privint = Generate(int.Parse(Z.Text));
            foreach(int x in privint)
            {
                priv += x.ToString() + ',';
            }
            priv = priv.Substring(0, priv.Length - 1);
            PrivateKey.Text = priv;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //расшифровать
        {
            DateTime t = DateTime.Now;
            string n = A.Text;
            string m = Module.Text;
            string cipher = TextToDecrypt.Text;
            int solv = knapsack.solve(Convert.ToInt32(n), Convert.ToInt32(m));
            string plain = knapsack.getknap(cipher, Convert.ToString(solv), m);
            string[] values = plain.Split(',');
            TextToEncrypt.Text = "";
            foreach (string value in values)
            {
                int v = int.Parse(value);
                string byteStr = knapsack.decipher(priv, v, int.Parse(Z.Text));
                int number = 0;
                for(int i=0; i<byteStr.Length;i++)
                {
                    if(byteStr[i] == '1')
                    {
                        number += (int)Math.Pow(2, i);
                    }
                }
                if (int.Parse(Z.Text) == 8)
                {
                        TextToEncrypt.Text += (char)(number);
                }
                else
                {
                    TextToEncrypt.Text += base64[number];
                }
                Time.Text = (DateTime.Now - t).ToString();
            }
        }
    }
}
