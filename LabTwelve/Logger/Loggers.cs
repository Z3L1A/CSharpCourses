using System;
using System.Diagnostics;

namespace LabTwelve.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string input)
        {
            ConsoleEx.ColorWrite("LOG: ", ConsoleColor.Yellow);
            Console.WriteLine($"{input}");
        }
    }

    public class DebugLogger : ILogger
    {
        public void Log(string input)
        {
            Debug.WriteLine($"LOG: {input}");
        }
    }

    public class TraceLogger : ILogger
    {
        public void Log(string input)
        {
            Trace.WriteLine($"LOG: {input}");
        }
    }
}