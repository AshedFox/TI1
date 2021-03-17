using System;
using System.Linq;

namespace Ciphers
{
    class ColumnarCipher
    {
        public string Encrypt(string input, string key)
        {
            (char symbol, int order)[] orderedKey = new (char, int)[key.Length];

            for (int i = 0;i < key.Length; i++)
            {
                orderedKey[i].order = i;
                orderedKey[i].symbol = key[i];
            }

            orderedKey = orderedKey.OrderBy(el => el.symbol).ToArray();

            string result = string.Empty;

            int height = (int)MathF.Ceiling(input.Length / (float)key.Length);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (i*key.Length + orderedKey[j].order < input.Length)
                    {
                        result += input[i * key.Length + orderedKey[j].order];
                    }
                    else
                    {
                        result += ' ';
                    }
                }
            }

            return result;
        }

        public string Decrypt(string input, string key)
        {
            bool[] isChecked = new bool[key.Length];
            string sortedKey = string.Copy(key);
            sortedKey = string.Concat(sortedKey.OrderBy(el => el));

            int[] order = new int[key.Length];

            for (int i = 0; i < sortedKey.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (!isChecked[j] && sortedKey[i] == key[j])
                    {
                        order[j] = i;
                        isChecked[j] = true;
                        break;
                    }
                }
            }

            string result = string.Empty;

            int height = (int)MathF.Ceiling(input.Length / (float)key.Length);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (i * key.Length + order[j] < input.Length)
                    {
                        result += input[i * key.Length + order[j]];
                    }
                    else
                    {
                        result += ' ';
                    }
                }
            }

            result = result.TrimEnd();

            return result;
        }
    }
}
