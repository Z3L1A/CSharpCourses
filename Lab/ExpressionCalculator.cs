using System;

namespace Lab
{
    public static class ExpressionCalculator
    {
        public static readonly Func<double, double, double, double, double> First = (a, b, c, d) => 
            Math.Log(Math.Abs(2 * a), Math.E) / 3 * Math.Tan(b) - Math.Sqrt(c / Math.Sqrt(Math.Abs(d)));

        public static readonly Func<double, double, double, double, double> Second = (a, b, c, d) =>
            2 * Math.Sqrt(Math.Sin(a) / Math.Abs(Math.Tan(b - a)) + Math.Log(c, Math.E) / d);

        public static readonly Func<double, double, double, double, double> Third = (a, b, c, d) =>
            2 * Math.Sqrt(Math.Tan(a) / Math.Abs(Math.Acos(b))) - 3 * Math.Cbrt(Math.Pow(Math.E, c - a) * Math.Sinh(d));

        public static double GetResultFromFirstExpression() => First(-2.86, 1.62, 10.874, -2.91);

        public static double GetResultFromSecondExpression() => Second(1.54, 0.49, 24.1, 0.87);

        public static double GetResultFromThirdExpression() => Third(1.25, 3.09, 4.25, 0.56);

        public static void ToConsole(this double result)
        {
            Console.WriteLine($"\nРезультат вычислений: {result}");
        }
    }
}