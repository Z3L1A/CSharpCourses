using System;
using System.Collections.Generic;
using System.Linq;

namespace LabNine
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomNumbers = RandomNumbersHelper.GetRandomNumbers();

            Console.WriteLine($"Числа: {randomNumbers.GetString()}\n");

            Console.WriteLine($"Числа меньше нуля: {randomNumbers.Where(x => x < 0).GetString()}");
            Console.WriteLine($"Числа кратные 2: {randomNumbers.Where(x => x % 2 == 0).GetString()}");
            Console.WriteLine($"Куб чисел: {randomNumbers.Select(x => Math.Pow(x, 3)).GetString()}");
            Console.WriteLine($"Минимальное число: {randomNumbers.Min()}");
            Console.WriteLine($"Сортировка по спаданию: {randomNumbers.OrderByDescending(x => x).GetString()}");

            Console.WriteLine();
            Console.WriteLine($"Кастомный куб чисел: {randomNumbers.TestSelect(x => Math.Pow(x, 3)).GetString()}");
            Console.WriteLine($"Кастомное умножение на 2: {randomNumbers.TestSelect(x => x + x).GetString()}");

            Console.WriteLine();
            Console.WriteLine($"Среднее значение: {randomNumbers.Average()}");
            Console.WriteLine($"Кастомное среднее значение: {randomNumbers.TestAverage()}");
            
            Console.ReadKey(true);
        }
    }

    public static class RandomNumbersHelper
    {
        public static List<int> GetRandomNumbers(int count = 10)
        {
            var random = new Random();
            return Enumerable.Range(0, count).Select(i => random.Next(-1000, 1000)).ToList();
        }
    }

    public static class IEnumerableEx
    {
        public static string GetString<T>(this IEnumerable<T> collection) => 
            string.Join(", ", collection);
    }

    public static class TestLinq
    {
        public static IEnumerable<TResult> TestSelect<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> func)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in collection)
            {
                //result.Add(func(item));
                yield return func(item);
            }
        }

        public static double TestAverage(this IEnumerable<int> collection)
        {
            var sum = 0;

            foreach (var item in collection)
            {
                sum += item;
            }

            return (double) sum / collection.Count();
        }
    }
}
