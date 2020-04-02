using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LabSix
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var key = ConsoleKey.Enter;
            while (key != ConsoleKey.Escape)
            { 
                var array = GetArrayFilledByRandomNumbers(100000);

                var selectionTask = Task.Run(() =>
                {
                    var selectionSortedArray = SelectionSortAsc(array);
                    Console.WriteLine("Sorting by selection for array with {0} elements ran for {1} ms with {2} swaps",
                        selectionSortedArray.NumberOfElements, selectionSortedArray.TimeOfExecution,
                        selectionSortedArray.NumberOfSwaps);
                });

                var insertionTask = Task.Run(() =>
                {
                    var insertionSortedArray = InsertionSortAsc(array);
                    Console.WriteLine("Sorting by insertion for array with {0} elements ran for {1} ms with {2} swaps",
                        insertionSortedArray.NumberOfElements, insertionSortedArray.TimeOfExecution,
                        insertionSortedArray.NumberOfSwaps);
                });

                await Task.WhenAll(selectionTask, insertionTask);

                key = Console.ReadKey(true).Key;
                Console.WriteLine();
            }

        }

        static SortedArrayInfo SelectionSortAsc(double[] inputArray)
        {
            var stopwatch = new Stopwatch();
            var array = (double[]) inputArray.Clone();
            var numberOfSwaps = 0L;
            
            stopwatch.Start();
            for (var i = 0; i < array.Length - 1; i++)
            {
                var min = i;
                for (var j = i+1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    var temp = array[i];
                    array[i] = array[min];
                    array[min] = temp;
                    numberOfSwaps++;
                }
            }

            return new SortedArrayInfo
            {
                Array = array,
                NumberOfSwaps = numberOfSwaps,
                TimeOfExecution = stopwatch.ElapsedMilliseconds
            };
        }

        static SortedArrayInfo InsertionSortAsc(double[] inputArray)
        {
            var stopwatch = new Stopwatch();
            var array = (double[]) inputArray.Clone();
            var numberOfSwaps = 0L;
            
            stopwatch.Start();
            
            //for (var i = 1; i < array.Length; i++)
            //{
            //    int j;
            //    var temp = array[i];
            //    for (j = i - 1; j >= 0; j--)
            //    {
            //        if (array[j] < temp)
            //            break;

            //        array[j + 1] = array[j];
            //        numberOfSwaps++;
            //    }
            //    array[j + 1] = temp;
            //}

            for (var i = 1; i < array.Length; i++)
            {
                for (var j = i; j > 0; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        var temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                        numberOfSwaps++;
                    }
                }
            }

            stopwatch.Stop();

            return new SortedArrayInfo
            {
                Array = array,
                NumberOfSwaps = numberOfSwaps,
                TimeOfExecution = stopwatch.ElapsedMilliseconds
            };
        }

        static double[] GetArrayFilledByRandomNumbers(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException("Provide array size greater than 1");
            }
            
            var random = new Random();
            var array = new double[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.NextDouble() * random.Next(int.MinValue, int.MaxValue);
            }

            return array;
        }
    }

    public class SortedArrayInfo
    {
        public double[] Array { get; set; }

        public long NumberOfSwaps { get; set; }

        public int NumberOfElements => Array.Length;

        public long TimeOfExecution { get; set; }
    }
}
