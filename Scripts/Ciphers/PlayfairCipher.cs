using System.Collections.Generic;
using System.Linq;

namespace Ciphers
{
    public class PlayfairCipher
    {
        struct Bigram
        {
            public char character1;
            public char character2;

            public Bigram(char character1, char character2)
            {
                this.character1 = character1;
                this.character2 = character2;
            }

            public override string ToString()
            {
                return character1.ToString() + character2.ToString();
            }
        }

        public string Encrypt(string input, string key)
        {
            char[,] cipherTable = FillCipherTable(key);

            string result = string.Empty;

            var bigrams = CreateBigrams(input);

            for (int i = 0; i < bigrams.Count; i++)
            {
                bigrams[i] = CipherBigram(cipherTable, bigrams[i]);
                result += bigrams[i].ToString();
            }

            return result;
        }

        Bigram CipherBigram(char[,] cipherTable, Bigram bigram)
        {
            int i1 = 0, i2 = 0, j1 = 0, j2 = 0;

            for (int i = 0; i < cipherTable.GetLength(1); i++)
            {
                for (int j = 0; j < cipherTable.GetLength(0); j++)
                {
                    if (cipherTable[i, j] == bigram.character1)
                    {
                        i1 = i;
                        j1 = j;
                    }
                    if (cipherTable[i, j] == bigram.character2)
                    {
                        i2 = i;
                        j2 = j;
                    }
                }
            }


            if (i1 == i2)
            {
                bigram.character1 = cipherTable[(i1 + 1) % (cipherTable.GetLength(1)), j1];
                bigram.character2 = cipherTable[(i2 + 1) % (cipherTable.GetLength(1)), j2];
            }
            else if (j1 == j2) 
            { 
                bigram.character1 = cipherTable[i1, (j1 + 1) % (cipherTable.GetLength(0))];
                bigram.character2 = cipherTable[i2, (j2 + 1) % (cipherTable.GetLength(0))];
            }
            else
            {
                bigram.character1 = cipherTable[i1, j2];
                bigram.character2 = cipherTable[i2, j1];
            }

            return bigram;
        }

        public string Decrypt(string input, string key)
        {
            char[,] cipherTable = FillCipherTable(key);
            input = input.Replace('j', 'i');

            string result = string.Empty;

            var bigrams = CreateBigrams(input);

            for (int i = 0; i < bigrams.Count; i++)
            {
                bigrams[i] = DecipherBigram(cipherTable, bigrams[i]);
                result += bigrams[i].ToString();
            }

            return result;
        }

        Bigram DecipherBigram(char[,] cipherTable, Bigram bigram)
        {
            int i1 = 0, i2 = 0, j1 = 0, j2 = 0;

            for (int i = 0; i < cipherTable.GetLength(1); i++)
            {
                for (int j = 0; j < cipherTable.GetLength(0); j++)
                {
                    if (cipherTable[i, j] == bigram.character1)
                    {
                        i1 = i;
                        j1 = j;
                    }
                    if (cipherTable[i, j] == bigram.character2)
                    {
                        i2 = i;
                        j2 = j;
                    }
                }
            }


            if (i1 == i2)
            {
                bigram.character1 = cipherTable[(i1 - 1 + cipherTable.GetLength(1)) % (cipherTable.GetLength(1)), j1];
                bigram.character2 = cipherTable[(i2 - 1 + cipherTable.GetLength(1)) % (cipherTable.GetLength(1)), j2];
            }
            else if (j1 == j2)
            {
                bigram.character1 = cipherTable[i1, (j1 - 1 + cipherTable.GetLength(0)) % (cipherTable.GetLength(0))];
                bigram.character2 = cipherTable[i2, (j2 - 1 + cipherTable.GetLength(0)) % (cipherTable.GetLength(0))];
            }
            else
            {
                bigram.character1 = cipherTable[i1, j2];
                bigram.character2 = cipherTable[i2, j1];
            }

            return bigram;
        }


        char[,] FillCipherTable(string key)
        {
            string alphabet = "abcdefghiklmnopqrstuvwxyz";

            var result = new char[5, 5];

            key = key.Replace('j', 'i');
            key += alphabet;
            key = string.Concat(key.ToLower().Where(ch => char.IsLetter(ch)).Distinct());

            for (int i = 0; i < result.GetLength(1); i++)
            {
                for (int j = 0; j < result.GetLength(0); j++)
                {
                    if (i * result.GetLength(1) + j < key.Length)
                    {
                        result[i, j] = key[i * result.GetLength(1) + j];
                    }
                }
            }
            return result;
        }

        List<Bigram> CreateBigrams(string input)
        {
            input = input.Replace('j', 'i');
            input = string.Concat(input.ToLower().Where(ch => (!char.IsPunctuation(ch) && !char.IsWhiteSpace(ch))));

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    input.Insert(i, "x");
                }
            }
            if (input.Length % 2 == 1)
            {
                input += 'x';
            }

            List<Bigram> result = new List<Bigram>();
            for (int i = 0; i < input.Length; i += 2)
            {
                result.Add(new Bigram(input[i], input[i + 1]));
            }

            return result;
        }
    }
}

