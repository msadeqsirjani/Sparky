using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class FibonacciTest
{
    private readonly Fibonacci _fibonacci;

    public FibonacciTest()
    {
        _fibonacci = new Fibonacci();
    }

    [Fact]
    public void Range_RangeInput_ReturnFibonacciRangeList()
    {
        var exceptedResult = new List<int> { 0, 1, 1, 2, 3, 5 };

        _fibonacci.Range = 6;

        var collection = _fibonacci.GetRange();

        collection.Should().BeEquivalentTo(exceptedResult);
        collection.Should().HaveCount(6);
        collection.Should().NotContain(4);
        collection.Should().Contain(3);
    }

    [Fact]
    public void GetRange_RangeWithValue1_ReturnFibonacciRangeList()
    {
        _fibonacci.Range = 1;
        var collection = _fibonacci.GetRange();

        collection.Should().NotBeEmpty();
        collection.Should().BeInAscendingOrder();
        collection.Should().Contain(0);
        collection.Should().BeEquivalentTo(new List<int> { 0 });
    }

    [Fact]
    public void GetRange_RangeWithValue6_ReturnFibonacciRangeList()
    {
        _fibonacci.Range = 6;
        var collection = _fibonacci.GetRange();

        collection.Should().Contain(3);
        collection.Should().HaveCount(6);
        collection.Should().NotContain(4);
        collection.Should().BeEquivalentTo(new List<int> { 0, 1, 1, 2, 3, 5 });
    }
}