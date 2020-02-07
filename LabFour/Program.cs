using System;
using System.Runtime.CompilerServices;

namespace LabFour
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Variants 5, 35, 65");

            var inputKey = ConsoleKey.Enter;

            while (inputKey != ConsoleKey.Escape)
            {
                Console.WriteLine("\nPress button to execute one of variants (press Esc to exit):");
                Console.WriteLine("1 - 5th variant \t 2 - 35th variant \t 3 - 65th variant");
                inputKey = Console.ReadKey(true).Key;

                switch (inputKey)
                {
                    case ConsoleKey.D1:
                        var array1 = GetRandomNumbers(10);
                        var array2 = GetRandomNumbers(-1235);
                        var array3 = GetRandomNumbers(56123156);
                        var array4 = new int[0];

                        Printer(GetSumOfNegativeElements, array1, array2, array3, array4, null, "sdfs");

                        break;
                    case ConsoleKey.D2:
                        Printer(GetSumOfNumbersOnSpecifiedIndexes,
                            GetRandomNumbers(15),
                            GetRandomNumbers(5),
                            GetRandomNumbers(6), 
                            GetRandomNumbers(7), 
                            null,
                            new int[0]);

                        break;
                    case ConsoleKey.D3:
                        Printer(GetInvertedArray, 
                            GetRandomNumbers(5),
                            new int[] {4, 5, 6});

                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("\nPlease press valid button");
                        break;
                }
            }
        }

        private static void Printer(Func<int[], long?> method, params object[] arrays)
        {
            for (int i = 0; i < arrays.Length; i++)
            {
                Console.Write($"{i+1}. ");

                try
                {
                    var result = method((int[]) arrays[i]);

                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void Printer(Func<int[], int[]> method, params object[] arrays)
        {
            for (int i = 0; i < arrays.Length; i++)
            {
                Console.Write($"{i+1}. ");

                try
                {
                    var numbers = (int[]) arrays[i];
                    var numbersClone = (int[]) numbers.Clone();
                    var result = method(numbersClone);

                    if (result != null)
                    {
                        Console.WriteLine($"Input array: \n{ArrayToString(numbers)}");
                        Console.WriteLine($"Output array: \n{ArrayToString(result)}");
                    }
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            string ArrayToString(int[] array)
            {
                var str = string.Empty;

                for (int i = 0; i < array.Length; i++)
                {
                    str += array[i] + (i < array.Length - 1 ? ", " : ".");
                }

                return str;
            }
        }

        private static int[] GetRandomNumbers(int numberOfElements)
        {
            int[] result;

            try
            {
                result = Randomizer.GetRandomNumbers(numberOfElements);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }

            return result;
        }

        private static long? GetSumOfNegativeElements(params int[] numbers)
        {
            long sum;
            try
            {
                sum = NegativeNumbersAdder.GetSumOfNegativeArrayElements(numbers);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return sum;
        }

        private static long? GetSumOfNumbersOnSpecifiedIndexes(params int[] numbers)
        {
            long sum;
            try
            {
                sum = NumbersAdder.GetSumOfNumbersOnSpecifiedIndexes(numbers);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return sum;
        }

        private static int[] GetInvertedArray(params int[] numbers)
        {
            int[] result;
            try
            {
                result = ArrayInverter.GetInvertedArray(numbers);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return result;
        }
    }

    public static class NegativeNumbersAdder
    {
        public static long GetSumOfNegativeArrayElements(int[] numbers)
        {
            if (numbers == null)
            {
                throw new NullReferenceException("Input array is not instantiated");
            }

            if (numbers.Length == 0)
            {
                throw new ArgumentException("Input array has zero length");
            }

            var result = 0;

            foreach (var number in numbers)
            {
                result += number < 0 ? number : 0;
            }

            return result;
        }
    }

    public static class NumbersAdder
    {
        public static long GetSumOfNumbersOnSpecifiedIndexes(int[] numbers)
        {
            if (numbers == null)
            {
                throw new NullReferenceException("Input array is not instantiated");
            }

            if (numbers.Length == 0)
            {
                throw new ArgumentException("Input array has zero length");
            }

            var result = 0;

            for (int i = 5; i < 11; i++)
            {
                try
                {
                    result += numbers[i];
                }
                catch (IndexOutOfRangeException)
                { }
            }

            return result;
        }
    }

    public static class ArrayInverter
    {
        public static int[] GetInvertedArray(int[] numbers)
        {
            if (numbers == null)
            {
                throw new NullReferenceException("Input array is not instantiated");
            }

            if (numbers.Length == 0)
            {
                throw new ArgumentException("Input array has zero length");
            }

            Array.Reverse(numbers);

            return numbers;
        }
    }

    public static class Randomizer
    {
        public static int[] GetRandomNumbers(int numberOfElements)
        {
            if (numberOfElements < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfElements), numberOfElements, "Method parameter should be greater than zero");
            }

            var random = new Random();

            var result = new int[numberOfElements];
            
            for (int i = 0; i < numberOfElements; i++)
            {
                result[i] = random.Next(int.MinValue, int.MaxValue);
            }

            return result;
        }
    }
}
