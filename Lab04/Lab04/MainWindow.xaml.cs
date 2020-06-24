using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Lab04
{
    public partial class MainWindow : Window
    {
        private readonly char[] _alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private readonly string _keyword = "SEPTILKO";
        private const int _key = 6;
        private char[] _newAlphabet = new char[26];
        private readonly string _keyTris = "NASTYA";
        private char[,] _table = new char[5, 5];

        public MainWindow()
        {
            InitializeComponent();

            tb_Encrypt.Text = ReadFromFile("en.txt");
            _newAlphabet = GetNewAlphabet(_keyword, _key);
        }

        private static string ReadFromFile(string path)
        {
            string text;
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                text = sr.ReadToEnd();
                text = text.Replace(" ", "");
                text = text.Replace(".", "");
                text = text.Replace(",", "");
                text = text.Replace("-", "");
                text = text.Replace(":", "");
                text = text.ToUpper();
            }
            return text;
        }

        public static bool IsAlphabet(string text, char[] alphabet)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(nameof(text));
            }

            bool result = true;
            foreach (char c in text)
            {
                if (!alphabet.Contains(c))
                {
                    result = false;
                }
            }
            return result;
        }

        private void CaesarEncrypt_Click(object sender, RoutedEventArgs e)//шифруем церазем
        {
            tb_Decrypt.Clear();
            if (!IsAlphabet(tb_Encrypt.Text, _alphabet))
            {
                throw new ArgumentException();
            }

            var timer = new Stopwatch();
            timer.Start();
            foreach(var ch in tb_Encrypt.Text.ToCharArray())
            {
                int index = Array.IndexOf(_alphabet, ch);
                tb_Decrypt.Text += _newAlphabet[index];
            }
            timer.Stop();
            var ts = timer.Elapsed;
            MessageBox.Show(string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        }

        private char[] GetNewAlphabet(string keyWord, int key) // создаёт новый алфавит с помощью ключа
        {
            key--;
            var result = new char[26];
            int beg = 0, current = key;

            for (int i = 0; i < keyWord.Length; i++)
            {
                result[current] = keyWord[i];
                current++;
            }
            // добавить буквы после ключевого слова
            for (int i = 0; i < _alphabet.Length; i++)
            {
                if (keyWord.Contains(_alphabet[i])) continue;
                if (current == result.Length)
                {
                    beg = i;
                    break;
                }
                result[current] = _alphabet[i];
                current++;
            }
            // добавить буквы перед ключевым словом
            current = 0;
            for (int i = beg; i < _alphabet.Length; i++)
            {
                if (keyWord.Contains(_alphabet[i])) continue;
                result[current] = _alphabet[i];
                current++;
            }
            return result;
        }

        private void CaesarDecrypt_Click(object sender, RoutedEventArgs e)//расшифруем церазем
        {
            tb_Encrypt.Clear();

            var timer = new Stopwatch();
            timer.Start();
            foreach (var ch in tb_Decrypt.Text.ToCharArray())
            {
                int index = Array.IndexOf(_newAlphabet, ch);
                tb_Encrypt.Text += _alphabet[index];
            }
            timer.Stop();
            var ts = timer.Elapsed;
            MessageBox.Show(string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        }

        private void TrisemusEncrypt_Click(object sender, RoutedEventArgs e)
        {
            tb_Decrypt.Clear();
            _table = GetTableTrisemus(_keyTris);

            var timer = new Stopwatch();
            timer.Start();
            foreach (var ch in tb_Encrypt.Text)
            {
                EncruptChar(ch);
            }
            timer.Stop();
            var ts = timer.Elapsed;
            MessageBox.Show(string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        }

        private void EncruptChar(char ch)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (ch == _table[i, j])
                    {
                        if (i != 4)
                        {
                            tb_Decrypt.Text += _table[i + 1, j];
                        }
                        else
                        {
                            tb_Decrypt.Text += _table[0, j];
                        }
                        return;
                    }
                }
            }
        }

        private char[,] GetTableTrisemus(string keyWord)
        {
            var table = new char[5, 5];
            var keyword = keyWord.ToCharArray();
            int k = 0;
            int a = 0;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (k < keyWord.Length)
                    {
                        table[i, j] = keyword[k];
                        k++;
                    }
                    else
                    {
                        a = CheckContains(a, keyword, _alphabet);
                        table[i, j] = _alphabet[a];
                        a++;
                    }
                }
            }
            return table;
        }

        private int CheckContains(int a, char[] keyword, char[] alphabet)
        {
            if (keyword.Contains(alphabet[a]))
            {
                a++;
                a = CheckContains(a, keyword, alphabet);
            }
            return a;
        }

        private void TrisemusDecrypt_Click(object sender, RoutedEventArgs e)
        {
            tb_Encrypt.Clear();

            var timer = new Stopwatch();
            timer.Start();
            foreach (var ch in tb_Decrypt.Text)
            {
                DecryptChar(ch);
            }
            timer.Stop();
            var ts = timer.Elapsed;
            MessageBox.Show(string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
        }

        private void DecryptChar(char ch)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (ch == _table[i, j])
                    {
                        if (i != 0)
                        {
                            tb_Encrypt.Text += _table[i - 1, j];
                        }
                        else
                        {
                            tb_Encrypt.Text += _table[4, j];
                        }
                        return;
                    }
                }
            }
        }
    }
}
