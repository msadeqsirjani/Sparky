using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class PersonTest
{
    private readonly Person _person;

    public PersonTest()
    {
        _person = new Person("Mohammad Sadeq", "Sirjani");
    }

    [Fact]
    public void Fullname_InputForenameAndSurname_ReturnCorrectFullname()
    {
        _person.Fullname.Should().Be("Mohammad Sadeq Sirjani");
        _person.Fullname.Should().StartWithEquivalentOf("mohammad");
        _person.Fullname.Should().ContainEquivalentOf("S");
        _person.Fullname.Should().MatchRegex(new Regex("\\D"));
    }

    [Fact]
    public void Nothing_CreatePerson_ReturnNull()
    {
        _person.Nothing.Should().BeNull();
        _person.Nothing.Should().IsNull();
    }

    [Fact]
    public void Discount_CreatePersonGetFullname_ReturnIntegerBetween15To25()
    {
        _person.Discount.Should().BeInRange(10, 25);

        var fullname = _person!.Fullname;

        _person.Discount.Should().BeInRange(10, 25);
    }

    [Fact]
    public void Fullname_ForenameIsEmptyOrNull_ReturnArgumentExceptionWithMessage()
    {
        var action = () =>
        {
            var person = new Person("", "Sirjani");

            var fullname = person.Fullname;
        };

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("Forename is null");
    }

    [Fact]
    public void Fullname_ForenameIsEmptyOrNull_ReturnArgumentExceptionWithoutMessage()
    {
        var action = () =>
        {
            var person = new Person("", "Sirjani");

            var fullname = person.Fullname;
        };

        action.Should()
            .Throw<ArgumentException>();
    }
}