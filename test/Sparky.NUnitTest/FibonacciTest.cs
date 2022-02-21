using System.Collections.Generic;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class FibonacciTest
{
    private Fibonacci _fibonacci = null!;

    [SetUp]
    public void Setup()
    {
        _fibonacci = new Fibonacci();
    }

    [Test]
    public void Range_RangeInput_ReturnFibonacciRangeList()
    {
        var exceptedResult = new List<int> { 0, 1, 1, 2, 3, 5 };

        _fibonacci.Range = 6;

        var collection = _fibonacci.GetRange();

        Assert.Multiple(() =>
        {
            Assert.That(collection, Is.EquivalentTo(exceptedResult));
            Assert.That(collection.Count, Is.EqualTo(6));
            Assert.That(collection, Has.No.Member(4));
            Assert.That(collection, Does.Contain(3));
        });
    }

    [Test]
    public void GetRange_RangeWithValue1_ReturnFibonacciRangeList()
    {
        _fibonacci.Range = 1;
        var collection = _fibonacci.GetRange();

        Assert.Multiple(() =>
        {
            Assert.That(collection, Is.Not.Empty);
            Assert.That(collection, Is.Ordered.Ascending);
            Assert.That(collection, Has.Member(0));
            Assert.That(collection, Is.EquivalentTo(new List<int> { 0 }));
        });
    }

    [Test]
    public void GetRange_RangeWithValue6_ReturnFibonacciRangeList()
    {
        _fibonacci.Range = 6;
        var collection = _fibonacci.GetRange();

        Assert.Multiple(() =>
        {
            Assert.That(collection, Has.Member(3));
            Assert.That(collection, Does.Contain(3));
            Assert.That(collection.Count, Is.EqualTo(6));
            Assert.That(collection, Has.No.Member(4));
            Assert.That(collection, Does.Not.Contain(4));
            Assert.That(collection, Is.EquivalentTo(new List<int> { 0, 1, 1, 2, 3, 5 }));
        });
    }
}