using System.Collections.Generic;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class CalculatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Sum_IntegerInput_ReturnCorrectOutput()
    {
        var result = Calculator.Sum(10, 20, 30);

        Assert.AreEqual(60, result);
        Assert.That(result, Is.EqualTo(60));
    }

    [Test]
    public void Sum_DoubleInput_ReturnCorrectOutput()
    {
        var result = Calculator.Sum(5.43, 10.53);

        Assert.AreEqual(15.96, result, 0.1);
    }

    [Test]
    [TestCase(5.4, 10.5, ExpectedResult = 15.9)]
    [TestCase(5.43, 10.43, ExpectedResult = 15.86)]
    [TestCase(5.49, 10.59, ExpectedResult = 16.08)]
    public double Sum_DoubleInput_ReturnCorrectOutput(params double[] args)
    {
        return Calculator.Sum(args);
    }

    [Test]
    [TestCase(10, 10, 10, 10, ExpectedResult = 40)]
    [TestCase(10, 20, 20, 10, ExpectedResult = 60)]
    public int Sum_IntegerInput_ReturnCorrectOutput(params int[] args)
    {
        return Calculator.Sum(args);
    }

    [Test]
    public void IsOdd_OddInput_ReturnTrueOutput()
    {
        var result = Calculator.IsOdd(11);

        Assert.AreEqual(true, result);
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    [TestCase(10)]
    [TestCase(12)]
    [TestCase(14)]
    [TestCase(16)]
    public void IsOdd_EvenInput_ReturnFalseOutput(int args)
    {
        var result = Calculator.IsOdd(args);

        Assert.IsFalse(result);
        Assert.That(result, Is.False);
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void OddRange_StartAndStopInput_ReturnCorrectOddRange()
    {
        var result = Calculator.OddRange(10, 15);

        List<int> expectedResult = new() { 11, 13, 15 };

        Assert.That(result, Is.EquivalentTo(expectedResult));
    }
}