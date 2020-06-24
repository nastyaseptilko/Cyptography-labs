using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab06
{
    public partial class MainWindow : Window
    {
        private char[,] _rotors =
        {
            { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
            { 'v', 'z', 'b', 'r', 'g', 'i', 't', 'y', 'u', 'p', 's', 'd', 'n', 'h', 'l', 'x', 'a', 'w', 'm', 'j', 'q', 'o', 'f', 'e', 'c', 'k' },
            { 'j', 'p', 'g', 'v', 'o', 'u', 'm', 'f', 'y', 'q', 'b', 'e', 'n', 'h', 'z', 'r', 'd', 'k', 'a', 's', 'x', 'l', 'i', 'c', 't', 'w' },
            { 'n', 'z', 'j', 'h', 'g', 'r', 'c', 'x', 'm', 'y', 's', 'w', 'b', 'o', 'u', 'f', 'a', 'i', 'v', 'l', 'p', 'e', 'k', 'q', 'd', 't' }
        };

        private readonly Dictionary<char, char> _reflector = new Dictionary<char, char>
        {
            ['a'] = 'r',
            ['b'] = 'd',
            ['c'] = 'o',
            ['e'] = 'j',
            ['f'] = 'n',
            ['g'] = 't',
            ['h'] = 'k',
            ['i'] = 'v',
            ['l'] = 'm',
            ['p'] = 'w',
            ['q'] = 'z',
            ['s'] = 'x',
            ['u'] = 'y'
            
        };

        private readonly int[] _rotorShifts = { 1, 2, 2 };

        public MainWindow()
        {
            InitializeComponent();
        }

        private string EncryptEnigma(string key)
        {
            char[] result = new char[key.Length];
            result = key.ToLower().ToCharArray();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = _rotors.GetLength(0) - 1; j > 0; j--)
                {
                    result[i] = _rotors[j, GetIndexByKey(result[i])];
                }

                foreach (var el in _reflector.Keys)
                {
                    if(result[i] == _reflector[el])
                    {
                        result[i] = el;
                    }else if (result[i] == el)
                    {
                        result[i] = _reflector[el];
                    }
                }

                for (int z = 0; z < _rotors.GetLength(0); z++)
                {
                    result[i] = _rotors[z, GetIndexByKey(result[i])];
                }

                for (int k = 1; k < _rotors.GetLength(0); k++)
                {
                    ShiftRotors(k);
                }
            }

            return new string(result);
        }

        private int GetIndexByKey(char key)
        {
            for (int i = 0; i < _rotors.GetLength(1); i++)
            {
                if (_rotors[0, i] == key)
                    return i;
            }

            throw new IndexOutOfRangeException();
        }

        private void ShiftRotors(int n)
        {
            char[] temp = new char[_rotors.GetLength(1)];
            int number = _rotorShifts[n - 1];
            for (int i = 0; i < _rotors.GetLength(1); i++)
            {
                temp[i] = (i - number < 0) ? 
                    _rotors[n, _rotors.GetLength(1) + (i - number)] : 
                    _rotors[n, i - number];
            }
            for (int i = 0; i < _rotors.GetLength(1); i++)
            {
                _rotors[n, i] = temp[i];
            }
        }

        private void Encypt_Click(object sender, RoutedEventArgs e)
        {
            tb_Result.Text = EncryptEnigma(tb_ForEncrypt.Text);
        }
    }
}
