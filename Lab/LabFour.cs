using System;

namespace Lab
{
    public class LabFour
    {
        
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
}