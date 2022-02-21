using System;
using Bongo.Models.ModelValidations;
using FluentAssertions;
using Xunit;

namespace Bongo.Models.Test;

public class DataInFutureAttributeTest
{
    [Fact]
    public void IsValid_DataInFuture_ReturnFalse()
    {
        DateInFutureAttribute attribute = new();

        attribute.IsValid(DateTime.Now).Should().BeFalse();
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(100, true)]
    [InlineData(1500, true)]
    [InlineData(-5362, false)]
    [InlineData(1532, true)]
    [InlineData(-6851, false)]
    public void IsValid_DateInFuture_ReturnCorrectResult(int interval, bool expected)
    {
        DateInFutureAttribute attribute = new(() => DateTime.Now);

        attribute.IsValid(DateTime.Now.AddSeconds(interval)).Should().Be(expected);
    }

    [Fact]
    public void IsValid_DateInFutureWithAnyDate_ReturnErrorMessage()
    {
        DateInFutureAttribute attribute = new(() => DateTime.Now);

        attribute.ErrorMessage.Should().Be("Date must be in the future");
    }
}