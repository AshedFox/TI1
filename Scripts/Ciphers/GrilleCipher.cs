using System;

namespace Ciphers
{
    public class GrilleCipher
    {
        public string Encrypt(string input)
        {
            int grilleHeight = (int)Math.Ceiling(Math.Sqrt(input.Length)) + 1;

            char[,] grille = CreateGrille(grilleHeight);   

            char[,] cipherMatrix = new char[grilleHeight, grilleHeight];

            int currentInputPos = 0;

            for (int side = 0; side < 4; side++)
            {
                for (int i = 0; i < grilleHeight; i++)
                {
                    for (int j = 0; j < grilleHeight; j++)
                    {
                        if (grille[i, j] == char.MaxValue)
                        {
                            if (currentInputPos < input.Length)
                            {
                                cipherMatrix[i, j] = input[currentInputPos++];
                            }
                        }
                    }
                }

                cipherMatrix = RotateMatrix(cipherMatrix);
            }

            string result = string.Empty;
            Random random = new Random();

            for (int i = 0; i < grilleHeight; i++)
            {
                for (int j = 0; j < grilleHeight; j++)
                {
                    if (cipherMatrix[i, j] != 0)
                    {
                        result += cipherMatrix[i, j];
                    }
                    else
                    {
                        int num = random.Next(0, 55);
                        char ch;
                        if (num == 0)
                            ch = (char)random.Next('0', '9');
                        else if (num <= 3)
                            ch = ' ';
                        else if (num <= 4)
                            ch = '?';                        
                        else if (num <= 5)
                            ch = '.';                        
                        else if (num <= 6)
                            ch = '!';                        
                        else if (num <= 7)
                            ch = ',';
                        else if (num <= 12)
                            ch = (char)random.Next('A', 'Z');
                        else if (num <= 17)
                            ch = (char)random.Next('А', 'Я');
                        else if (num <= 36)
                            ch = (char)random.Next('а', 'я');
                        else
                            ch = (char)random.Next('a', 'z');

                        result += ch;
                    }
                }
            }

            return result;
        }

        char[,] CreateGrille(int grilleHeight)
        {
            char[,] grille = new char[grilleHeight, grilleHeight];
            int step = 0;

            for (int cycle = 0; cycle < (int)Math.Ceiling((float)grilleHeight / 2); cycle++)
            {
                for (int i = cycle; i < grilleHeight - cycle - 1; i++)
                {
                    grille[cycle, i] = (char)(i + step + 1);
                }
                for (int i = cycle; i < grilleHeight - cycle - 1; i++)
                {
                    grille[grilleHeight - i - 1, cycle] = (char)(i + step + 1);
                }
                for (int i = grilleHeight - cycle; i > cycle + 1; i--)
                {
                    grille[grilleHeight - cycle - 1, grilleHeight - i + 1] = (char)(i + step - 1);
                }
                for (int i = cycle; i < grilleHeight - cycle - 1; i++)
                {
                    grille[i, grilleHeight - cycle - 1] = (char)(i + step + 1);
                }

                step += grilleHeight - (cycle + 1) * 2;
            }

            int maxVal = 1;
            for (int i = 0; i < (int)Math.Ceiling((float)grilleHeight / 2) - 1; i++)
            {
                maxVal += (i + 1) * 2;
            }
            if (grilleHeight % 2 == 0)
            {
                maxVal++;
            }

            for (int curVal = 1; curVal <= maxVal; curVal++)
            {
                FillGrilleCell(grille, curVal);

                grille = RotateMatrix(grille);
            }

            return grille;
        }

        void FillGrilleCell(char[,] grille, int curVal)
        {
            for (int i = 0; i < grille.GetLength(1); i++)
            {
                for (int j = 0; j < grille.GetLength(0); j++)
                {
                    if (grille[i, j] == curVal)
                    {
                        grille[i, j] = char.MaxValue;
                        return;
                    }
                }
            }
        }

        char[,] RotateMatrix(char[,] matrix)
        {
            var result = new char[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(1); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    result[i, j] = matrix[j, matrix.GetLength(1) - i - 1];

            return result;
        }

        public string Decrypt(string input)
        {
            int grilleHeight = (int)Math.Ceiling(Math.Sqrt(input.Length));

            char[,] grille = CreateGrille(grilleHeight);

            char[,] cipherMatrix = new char[grilleHeight, grilleHeight];

            for (int i = 0; i < grilleHeight; i++)
            {
                for (int j = 0; j < grilleHeight; j++)
                {
                    if (i * grilleHeight + j < input.Length)
                    {
                        cipherMatrix[i, j] = input[i * grilleHeight + j];
                    }
                }
            }

            string result = string.Empty;

            for (int side = 0; side < 4; side++)
            {
                for (int i = 0; i < grilleHeight; i++)
                {
                    for (int j = 0; j < grilleHeight; j++)
                    {
                        if (grille[i, j] == char.MaxValue)
                        {
                            result += cipherMatrix[i, j];
                        }
                    }
                }

                cipherMatrix = RotateMatrix(cipherMatrix);
            }

            return result;
        }
    }
}
