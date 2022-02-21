using FluentAssertions;
using Moq;
using Xunit;

namespace Sparky.XUnitTest;

public class BankAccountTest
{
    private BankAccount _bankAccount = null!;

    [Fact]
    public void DepositWithFakeClass_Add100_ReturnSuccess()
    {
        _bankAccount = new BankAccount(new FakeLog());

        var result = _bankAccount.Deposit(100);

        result.Should().BeTrue();
    }

    [Fact]
    public void DepositWithMock_Add100_ReturnSuccess()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToConsole("Deposit Invoked"));

        _bankAccount = new BankAccount(log.Object);

        var result = _bankAccount.Deposit(100);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(200, 100, true)]
    [InlineData(450, 250, true)]
    [InlineData(6750, 3526, true)]
    [InlineData(800, 1000, false)]
    [InlineData(952, 1113, false)]
    public void WithDraw_WithDrawWithBalance_ReturnTrue(int balance, int withdraw, bool expected)
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.Is<Money>(y => y.Amount > 0))).Returns(true);

        _bankAccount = new BankAccount(log.Object);
        _bankAccount.Deposit(balance);

        _bankAccount.WithDraw(withdraw).Should().Be(expected);
    }

    [Fact]
    public void WithDraw_WithDraw300WithBalance250_ReturnFalse()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsAny<Money>())).Returns(false);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(250);

        var result = _bankAccount.WithDraw(300);

        result.Should().BeFalse();
    }

    [Fact]
    public void WithDraw_WithDraw450WithBalance600_ReturnTrue()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsAny<Money>())).Returns(true);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(600);

        var result = _bankAccount.WithDraw(450);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1200, 150, true)]
    [InlineData(2600, 15000, false)]
    public void WithDraw_WithDrawWithBalance_ReturnCorrectValue(double balance, double withdraw, bool expected)
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.Is<Money>(x => x.Amount > 0))).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsInRange(new Money(double.MinValue), new Money(-1), Range.Inclusive))).Returns(false);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(balance);

        _bankAccount.WithDraw(withdraw).Should().Be(expected);
    }

    [Fact]
    public void WithLogMessage_ReturnMessage()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageReturnString(It.IsAny<string>()))
            .Returns((string args) => args.ToLowerInvariant());

        log.Object.WithLogMessageReturnString("heLLO").Should().BeEquivalentTo("hello");
    }

    [Fact]
    public void WithLogMessage_ReturnBoolean()
    {
        var log = new Mock<ILog>();

        string? value = null;

        log.Setup(x => x.WithLogMessageReturnBoolean(It.IsAny<string>(), out value)).Returns(true);

        var result = log.Object.WithLogMessageReturnBoolean("Sadeq", out var greeting);

        result.Should().BeTrue();
        value.Should().Be(greeting);
    }

    [Fact]
    public void WithLogMessageWithRef_ReturnBoolean()
    {
        var log = new Mock<ILog>();

        Customer customer = new("Mohammad Sadeq", "Sirjani", 100);
        Customer notUsedCustomer = new("Mohammad Sadeq", "Sirjani", 100);

        log.Setup(x => x.WithLogMessageWithRef(ref customer)).Returns(true);

        log.Object.WithLogMessageWithRef(ref customer).Should().BeTrue();
        log.Object.WithLogMessageWithRef(ref notUsedCustomer).Should().BeFalse();
    }

    [Fact]
    public void WithLogLevelAndLogLevelDescription_Return10AndWarning()
    {
        var log = new Mock<ILog>();

        log.SetupAllProperties();

        log.Setup(x => x.LogLevel).Returns(10);
        log.Setup(x => x.LogLevelDescription).Returns("Warning");

        log.Object.LogLevel = 101;

        log.Object.LogLevel.Should().Be(101);
        log.Object.LogLevelDescription.Should().Be("Warning");
    }

    [Fact]
    public void WithLogMessageUsingCallback_ReturnVoid()
    {
        var log = new Mock<ILog>();

        var counter = 10;

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>()))
            .Callback(() => counter++)
            .Returns(true)
            .Callback(() => counter++);

        log.Object.WithLogMessageToDatabase("Hello World!");

        counter.Should().Be(12);
    }

    [Fact]
    public void WithLogMessageUsingVerify_ReturnVoid()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToConsole(It.IsAny<string>()));

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(100);

        _bankAccount.Balance.Amount.Should().Be(100);

        log.Verify(x => x.WithLogMessageToConsole(It.IsAny<string>()), Times.Exactly(2));
        log.Verify(x => x.WithLogMessageToConsole("Test Verification"), Times.AtLeastOnce);
        log.VerifySet(x => x.LogLevel = 102, Times.Once);
        log.VerifyGet(x => x.LogLevel, Times.Once);
    }
}