using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class PersonTest
{
    private Person? _person;

    [SetUp]
    public void Setup()
    {
        _person = new Person("Mohammad Sadeq", "Sirjani");
    }

    [Test]
    public void Fullname_InputForenameAndSurname_ReturnCorrectFullname()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_person!.Fullname, Is.EqualTo("Mohammad Sadeq Sirjani"));
            Assert.That(_person!.Fullname, Does.StartWith("mohammad").IgnoreCase);
            Assert.That(_person!.Fullname, Does.Contain("S").IgnoreCase);
            Assert.That(_person!.Fullname, Does.Match(new Regex("\\D")));
        });
    }

    [Test]
    public void Nothing_CreatePerson_ReturnNull()
    {
        Assert.That(_person!.Nothing, Is.Null);
        Assert.IsNull(_person!.Nothing);
    }

    [Test]
    public void Discount_CreatePersonGetFullname_ReturnIntegerBetween15To25()
    {
        Assert.That(_person!.Discount, Is.InRange(10, 25));

        var fullname = _person!.Fullname;

        Assert.That(_person!.Discount, Is.InRange(10, 25));
    }

    [Test]
    public void Fullname_ForenameIsEmptyOrNull_ReturnArgumentExceptionWithMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var person = new Person("", "Sirjani");

            var fullname = person.Fullname;
        });

        Assert.AreEqual("Forename is null", exception.Message);

        Assert.That(() => new Person("", "Sirjani").Fullname,
            Throws.ArgumentException.With.Message.EqualTo("Forename is null"));
    }

    [Test]
    public void Fullname_ForenameIsEmptyOrNull_ReturnArgumentExceptionWithoutMessage()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var person = new Person("", "Sirjani");

            var fullname = person.Fullname;
        });

        Assert.That(() => new Person("", "Sirjani").Fullname, Throws.ArgumentException);
    }
}