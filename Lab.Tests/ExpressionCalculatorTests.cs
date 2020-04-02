using System;
using NUnit.Framework;

namespace Lab.Tests
{
    [TestFixture]
    public class ExpressionCalculatorTests
    {
        [TestCase(-2.86, 1.62, 10.874, -2.91, -14.3298)]
        [TestCase(100, 20, 30, 40, 1.7731)]
        [TestCase(0, 0, 0, 0, double.NaN)]
        public void FirstExpressionTest(double a, double b, double c, double d, double expectedValue)
        {
            Assert.AreEqual(expectedValue, ExpressionCalculator.First(a, b, c, d), 0.0001);
        }

        [TestCase(1.54, 0.49, 24.1, 0.87, 4.1139)]
        [TestCase(100, 20, 30, 40, 0.3393)]
        [TestCase(0, 0, 0, 0, double.NaN)]
        public void SecondExpressionTest(double a, double b, double c, double d, double expectedValue)
        {
            Assert.AreEqual(expectedValue, ExpressionCalculator.Second(a, b, c, d), 0.0001);
        }

        [TestCase(1.25, 3.09, 4.25, 0.56, double.NaN)]
        [TestCase(0.153, 0.632, 0.51, 0.86, -2.5109)]
        [TestCase(0, 0, 0, 0, 0)]
        public void ThirdExpressionTest(double a, double b, double c, double d, double expectedValue)
        {
            Assert.AreEqual(expectedValue, ExpressionCalculator.Third(a, b, c, d), 0.0001);
        }
    }
}