using System;

namespace LabFive
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix1 = Randomizer.GetMatrixWithRandomNumbers(3, 5);
            var matrix2 = Randomizer.GetMatrixWithRandomNumbers(5, 3);

            Console.WriteLine("First matrix:");
            Console.WriteLine("Input:");
            Printer(matrix1);

            Console.WriteLine("Output:");
            ClearNe(matrix1);
            Printer(matrix1);

            Console.WriteLine("Second matrix:");
            Console.WriteLine("Input:");
            Printer(matrix2);

            Console.WriteLine("Output:");
            ClearNe(matrix2);
            Printer(matrix2);
        }

        static void ClearNe(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (i <= j)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
        }

        static void Printer(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write($"( ");
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($"{matrix[i][j]} ");
                }

                Console.WriteLine(")\n");
            }
        }
    }

    public class Randomizer
    {
        public static int[][] GetMatrixWithRandomNumbers(int lines, int columns)
        {
            if (lines < 2 || columns < 2)
            {
                throw new ArgumentException("Parameters should be greater than 2.");
            }

            var random = new Random();

            var result = new int[lines][];
            
            for (int i = 0; i < lines; i++)
            {
                result[i] = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    result[i][j] = random.Next(-10, 10);
                }
            }

            return result;
        }
    }
}
