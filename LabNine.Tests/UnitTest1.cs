using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace LabNine.Tests
{
    public class Tests
    {
        [Test]
        public void TestSelectOnDoubleTest()
        {
            // Arrange
            Func<int, int> triple = a => a * 3;
            var numbers = RandomNumbersHelper.GetRandomNumbers();

            // Act
            var actualResult = numbers.TestSelect(triple);
            var expectedResult = numbers.Select(triple);

            // Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void TestSelectOnCubeTest()
        {
            // Arrange
            Func<int, double> cube = a => Math.Pow(a, 3);
            var numbers = RandomNumbersHelper.GetRandomNumbers();

            // Act
            var actualResult = numbers.TestSelect(cube);
            var expectedResult = numbers.Select(cube);

            // Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void TestAverageMethodTest()
        {
            // Arrange
            var numbers = RandomNumbersHelper.GetRandomNumbers();

            // Act
            var actualResult = numbers.TestAverage();
            var expectedResult = numbers.Average();

            // Assert
            expectedResult.Should().Be(actualResult);
        }
    }
}