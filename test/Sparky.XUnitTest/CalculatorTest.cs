using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class CalculatorTest
{
    [Fact]
    public void Sum_IntegerInput_ReturnCorrectOutput1()
    {
        var result = Calculator.Sum(10, 20, 30);

        result.Should().Be(60);
    }

    [Fact]
    public void Sum_DoubleInput_ReturnCorrectOutput2()
    {
        var result = Calculator.Sum(5.43, 10.53);

        result.Should().BeApproximately(15.96, 1);
    }

    [Theory]
    [InlineData(15.9, 5.4, 10.5)]
    [InlineData(15.86, 5.43, 10.43)]
    [InlineData(16.08, 5.49, 10.59)]
    public void Sum_DoubleInput_ReturnCorrectOutput3(double expected, params double[] args)
    {
        Calculator.Sum(args).Should().BeApproximately(expected, 2);
    }

    [Theory]
    [InlineData(40, 10, 10, 10, 10)]
    [InlineData(60, 10, 20, 20, 10)]
    public void Sum_IntegerInput_ReturnCorrectOutput4(int expected, params int[] args)
    {
        Calculator.Sum(args).Should().Be(expected);
    }

    [Fact]
    public void IsOdd_OddInput_ReturnTrueOutput()
    {
        var result = Calculator.IsOdd(11);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(12)]
    [InlineData(14)]
    [InlineData(16)]
    public void IsOdd_EvenInput_ReturnFalseOutput(int args)
    {
        var result = Calculator.IsOdd(args);

        result.Should().BeFalse();
    }

    [Fact]
    public void OddRange_StartAndStopInput_ReturnCorrectOddRange()
    {
        var result = Calculator.OddRange(10, 15);

        List<int> expectedResult = new() { 11, 13, 15 };

        result.Should().BeEquivalentTo(expectedResult);
        result.Should().Contain(11);
        result.Should().NotBeEmpty();
        result.Should().NotContain(7);
        result.Should().BeInAscendingOrder();
        result.Should().OnlyHaveUniqueItems();
    }
}