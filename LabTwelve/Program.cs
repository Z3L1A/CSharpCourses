using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using LabTwelve.Logger;

namespace LabTwelve
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var key = ConsoleKey.Enter;
            while (key != ConsoleKey.Escape)
            {
                //// Cache and log
                //var requestProcessor = new RequestProcessor();
                //var cachedRequestProcessor = new CachedRequestProcessor(requestProcessor);
                //var loggedAndCachedRequestProcessor = new RequestProcessorLogger(cachedRequestProcessor, new ConsoleLogger());

                ////var result = await decoratedRequestProcessor.Process("Hello!");
                ////Console.WriteLine($"Result: {result}");

                //await loggedAndCachedRequestProcessor.Process("Hello!");
                //await loggedAndCachedRequestProcessor.Process("Hello!");
                //Console.WriteLine();

                //var secondCached = new RequestProcessorLogger(new CachedRequestProcessor(requestProcessor), new ConsoleLogger());
                //Console.WriteLine(await secondCached.Process("Hello!"));

                //// Retry
                //var withErrors = new RequestProcessorWithErrors();
                //var withRetry = new RequestProcessorWithRetry(withErrors, 3);

                //Console.WriteLine(await withRetry.Process("Success!"));

                // With custom exception
                // Retry
                var withEx = new RequestProcessorWithCustomException(new RequestProcessorWithErrors());
                var withRetryAndEx = new RequestProcessorWithRetry(withEx, 2);
                try
                {
                    Console.WriteLine(await withRetryAndEx.Process("Success!"));
                }
                catch (RequestException e)
                {
                    ConsoleEx.ColorWrite("\nFailed!\n", ConsoleColor.Magenta);
                    Console.WriteLine(e);
                }

                key = Console.ReadKey(true).Key;
            }
        }
    }

    public abstract class RequestProcessorAbstract
    {
        public abstract Task<string> Process(string input);
    }

    public class RequestProcessor : RequestProcessorAbstract
    {
        public override async Task<string> Process(string input)
        {
            await Task.Delay(100);
            return $"Ha-ha: {input}";
        }
    }

    public class RequestProcessorWithErrors : RequestProcessorAbstract
    {
        public override async Task<string> Process(string input)
        {
            await Task.Delay(100);

            var random = new Random();

            if (random.Next() % 2 != 0)
            {
                throw new ArgumentException("Exception!");
            }

            return $"Ha-ha: {input}";
        }
    }

    public abstract class RequestProcessorDecorator : RequestProcessorAbstract
    {
        protected readonly RequestProcessorAbstract RequestProcessorAbstract;

        public RequestProcessorDecorator(RequestProcessorAbstract requestProcessorAbstract)
        {
            RequestProcessorAbstract = requestProcessorAbstract ?? throw new ArgumentNullException($"{nameof(RequestProcessorAbstract)} could not be null");
        }

        public override Task<string> Process(string input)
        {
            return RequestProcessorAbstract.Process(input);
        }
    }

    public class RequestProcessorLogger : RequestProcessorDecorator
    {
        private readonly ILogger _logger;

        public RequestProcessorLogger(RequestProcessorAbstract requestProcessorAbstract, ILogger logger) : base(requestProcessorAbstract)
        {
            _logger = logger ?? throw new ArgumentNullException($"{nameof(ILogger)} could not be null");
        }

        public override async Task<string> Process(string input)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await base.Process(input);
            stopwatch.Stop();
            
            _logger.Log($"Request '{input}' processed with result {result} in {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }
    }

    public class CachedRequestProcessor : RequestProcessorDecorator
    {
        private readonly StringCache _cache;

        public CachedRequestProcessor(RequestProcessorAbstract requestProcessorAbstract) : base(requestProcessorAbstract)
        {
            _cache = StringCache.GetInstance();
        }

        public override async Task<string> Process(string input)
        {
            return _cache.TryGet(input, out var value) ? value : await ProcessAndCache();

            //if (_cache.TryGet(input, out var value))
            //{
            //    return value;
            //}
            //else
            //{
            //    await ProcessAndCache();
            //}

            //return _cache.MaybeGet(input).Match(async () => await ProcessAndCache(), Task.FromResult);

            async Task<string> ProcessAndCache()
            {
                var result = await base.Process(input);
                _cache.Add(input, result);
                return result;
            }
        }
    }

    public class RequestProcessorWithRetry : RequestProcessorDecorator
    {
        private readonly int _retryCount;

        public RequestProcessorWithRetry(RequestProcessorAbstract requestProcessorAbstract, int retryCount) : base(requestProcessorAbstract)
        {
            _retryCount = retryCount;
        }

        public override async Task<string> Process(string input)
        {
            var currentRetry = 0;

            for (;;)
            {
                try
                {
                    return await base.Process(input);
                }
                catch (Exception e)
                {
                    currentRetry++;

                    ConsoleEx.ColorWrite("ERR: ", ConsoleColor.Red);
                    Console.WriteLine($"Oops! {e}");
                    
                    if (currentRetry > _retryCount)
                    {
                        throw;
                    }
                    else
                    {
                        Console.WriteLine($"Retry {currentRetry} of {_retryCount}");
                    }
                }
            }
        }
    }

    public class RequestProcessorWithCustomException : RequestProcessorDecorator
    {
        public RequestProcessorWithCustomException(RequestProcessorAbstract requestProcessorAbstract) : base(requestProcessorAbstract)
        {
        }

        public override async Task<string> Process(string input)
        {
            try
            {
                return await base.Process(input);
            }
            catch (Exception e)
            {
                throw new RequestException(input, e);
            }
        }
    }

    public static class ConsoleEx
    {
        public static void ColorWrite(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}
