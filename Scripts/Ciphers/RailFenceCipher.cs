using System;

namespace Ciphers
{
    class RailFenceCipher
    {
        string FillResultString(char [,] matrix)
        {
            string result = string.Empty;

            for (int row = 0; row < matrix.GetLength(1); row++)
            {
                for (int col = 0; col < matrix.GetLength(0); col++)
                {
                    if (matrix[col, row] != '\0')
                    {
                        result += matrix[col, row];
                    }
                }
            }

            return result;
        }

        char[,] TransposeMatrix(char[,] matrix)
        {
            char[,] resultMatrix = new char[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    resultMatrix[j, i] = matrix[i, j];
                }
            }

            return resultMatrix;
        }

        public string Encrypt(string input, int height) 
        {
            if (height < 2)
            {
                Console.WriteLine("Incorrect height");
                return input;
            }

            char[,] matrix = new char[input.Length, height];

            int direction = 1;
            int row = 0;
            for (int col = 0; col < input.Length; col++)
            {
                matrix[col, row] = input[col];

                row += direction;

                if (row >= matrix.GetLength(1))
                {
                    direction = -1;
                    row -= 2;
                }
                else if (row < 0)
                {
                    direction = 1;
                    row += 2;
                }
            }

            return FillResultString(matrix);
        }

        public string Decrypt(string input, int height)
        {
            if (height < 2)
            {
                Console.WriteLine("Incorrect height");
                return input;
            }

            char[,] matrix = new char[input.Length, height];

            int id = 0;

            for (int i = 0; i < matrix.GetLength(1); i++) {
                int direction = 1;
                int row = 0;
                for (int col = 0; col < input.Length; col++)
                {
                    if (i == row)
                        matrix[col, row] = input[id++];

                    row += direction;

                    if (row >= matrix.GetLength(1))
                    {
                        direction = -1;
                        row -= 2;
                    }
                    else if (row < 0)
                    {
                        direction = 1;
                        row += 2;
                    }
                }
            }

            matrix = TransposeMatrix(matrix);

            return FillResultString(matrix);
        }
    }
}
