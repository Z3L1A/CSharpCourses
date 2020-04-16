using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabEleven
{
    class Program
    {
        private static object _locker = new object();

        static async Task Main(string[] args)
        {
            var dirPath = @""; // provide some directory path

            var dir = new DirectoryInfo(dirPath);

            var files = dir.EnumerateFiles("*.cs", SearchOption.AllDirectories);

            var dict = new ConcurrentDictionary<string, int>();
            //var dict = new Dictionary<string, int>();

            //var i = 0;

            //var tasksFoo = Enumerable.Range(0, 100000).Select(e =>
            //    Task.Run(() =>
            //    {
            //        //Interlocked.Increment(ref i);

            //        i++;

            //        //lock (_locker)
            //        //{
            //        //    i++;
            //        //}
            //    }));


            //await Task.WhenAll(tasksFoo);

            //Console.WriteLine(i);

            var tasks = new List<Task>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.ForEach(files, (fileInfo) =>
            {
                tasks.Add(Do(fileInfo));
            });

            //foreach (var fileInfo in files)
            //{
            //    tasks.Add(Do(fileInfo));
            //    //await Do(fileInfo);
            //}

            await Task.WhenAll(tasks);

            stopWatch.Stop();

            Console.WriteLine(string.Join(", ", dict.TopWordsString(80)));

            Console.WriteLine($"\nElapsed: {stopWatch.ElapsedMilliseconds}");
            //Console.WriteLine(string.Join(", ", dict.ToKeyValueString()));

            async Task Do(FileInfo fileInfo)
            {
                await foreach (var word in WordCalculator.GetUniqueWordsFromFileAsync(fileInfo))
                {
                    dict.AddOrUpdate(word, 1, (s, i) => i + 1);

                    //Console.WriteLine($"{word}: {dict[word]}");

                    //lock (_locker)
                    //{
                    ////    if (dict.TryAdd(word, 1))
                    ////    {
                    ////        dict[word]++;
                    ////    }

                    //    dict.AddOrUpdate(word, 1, (s, i) => i + 1);
                    //}
                }
            }
        }
    }

    public class WordCalculator
    {
        public static async IAsyncEnumerable<string> GetUniqueWordsFromFileAsync(FileInfo fileName)
        {
            //Console.WriteLine($"LOG: Reading file '{fileName.Name}'");

            using (StreamReader sr = new StreamReader(fileName.FullName))
            {
                string line;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    line = new string(line.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());

                    //yield return line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(w => w.ToLower());
                    foreach (var word in line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(w => w.ToLower()))
                    {
                        yield return word;
                    }
                }
            }

            //Console.WriteLine($"LOG: End of file '{fileName.Name}'");
        }
    }

    public static class ConcurrentDictionaryEx
    {
        public static IEnumerable<string> ToKeyValueString(this ConcurrentDictionary<string, int> dictionary)
        {
            foreach (var item in dictionary.OrderByDescending(item => item.Value))
            {
                yield return $"{item.Key} - {item.Value}";
            }
        }

        public static IEnumerable<string> TopWordsString(this ConcurrentDictionary<string, int> dictionary, int count)
        {
            foreach (var item in
                dictionary.OrderByDescending(item => item.Value).ThenBy(item => item.Key))
            {
                yield return $"{item.Key} - {item.Value}";

                if (item.Value < count)
                {
                    break;
                }
            }
        }

        public static IEnumerable<string> TopWordsString(this Dictionary<string, int> dictionary, int count)
        {
            foreach (var item in
                dictionary.OrderByDescending(item => item.Value).ThenBy(item => item.Key))
            {
                yield return $"{item.Key} - {item.Value}";

                if (item.Value < count)
                {
                    break;
                }
            }
        }
    }
}