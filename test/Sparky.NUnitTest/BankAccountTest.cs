using Moq;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class BankAccountTest
{
    private BankAccount _bankAccount = null!;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void DepositWithFakeClass_Add100_ReturnSuccess()
    {
        _bankAccount = new BankAccount(new FakeLog());

        var result = _bankAccount.Deposit(100);

        Assert.That(result, Is.True);
    }

    [Test]
    public void DepositWithMock_Add100_ReturnSuccess()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToConsole("Deposit Invoked"));

        _bankAccount = new BankAccount(log.Object);

        var result = _bankAccount.Deposit(100);

        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(200, 100, ExpectedResult = true)]
    [TestCase(450, 250, ExpectedResult = true)]
    [TestCase(6750, 3526, ExpectedResult = true)]
    [TestCase(800, 1000, ExpectedResult = false)]
    [TestCase(952, 1113, ExpectedResult = false)]
    public bool WithDraw_WithDrawWithBalance_ReturnTrue(int balance, int withdraw)
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.Is<Money>(y => y.Amount > 0))).Returns(true);

        _bankAccount = new BankAccount(log.Object);
        _bankAccount.Deposit(balance);

        return _bankAccount.WithDraw(withdraw);
    }

    [Test]
    public void WithDraw_WithDraw300WithBalance250_ReturnFalse()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsAny<Money>())).Returns(false);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(250);

        var result = _bankAccount.WithDraw(300);

        Assert.That(result, Is.False);
    }

    [Test]
    public void WithDraw_WithDraw450WithBalance600_ReturnTrue()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsAny<Money>())).Returns(true);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(600);

        var result = _bankAccount.WithDraw(450);

        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(1200, 150, ExpectedResult = true)]
    [TestCase(2600, 15000, ExpectedResult = false)]
    public bool WithDraw_WithDrawWithBalance_ReturnCorrectValue(double balance, double withdraw)
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>())).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.Is<Money>(x => x.Amount > 0))).Returns(true);
        log.Setup(x => x.WithLogMessageAfterWithdraw(It.IsInRange(new Money(double.MinValue), new Money(-1), Range.Inclusive))).Returns(false);

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(balance);

        return _bankAccount.WithDraw(withdraw);
    }

    [Test]
    public void WithLogMessage_ReturnMessage()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageReturnString(It.IsAny<string>()))
            .Returns((string args) => args.ToLowerInvariant());

        Assert.That(log.Object.WithLogMessageReturnString("heLLO"), Is.EquivalentTo("hello"));
    }

    [Test]
    public void WithLogMessage_ReturnBoolean()
    {
        var log = new Mock<ILog>();

        string? value = null;

        log.Setup(x => x.WithLogMessageReturnBoolean(It.IsAny<string>(), out value)).Returns(true);

        var result = log.Object.WithLogMessageReturnBoolean("Sadeq", out var greeting);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(greeting));
    }

    [Test]
    public void WithLogMessageWithRef_ReturnBoolean()
    {
        var log = new Mock<ILog>();

        Customer customer = new("Mohammad Sadeq", "Sirjani", 100);
        Customer notUsedCustomer = new("Mohammad Sadeq", "Sirjani", 100);

        log.Setup(x => x.WithLogMessageWithRef(ref customer)).Returns(true);

        Assert.That(log.Object.WithLogMessageWithRef(ref customer), Is.True);
        Assert.That(log.Object.WithLogMessageWithRef(ref notUsedCustomer), Is.False);
    }

    [Test]
    public void WithLogLevelAndLogLevelDescription_Return10AndWarning()
    {
        var log = new Mock<ILog>();

        log.SetupAllProperties();

        log.Setup(x => x.LogLevel).Returns(10);
        log.Setup(x => x.LogLevelDescription).Returns("Warning");

        log.Object.LogLevel = 101;

        Assert.That(log.Object.LogLevel, Is.EqualTo(101));
        Assert.That(log.Object.LogLevelDescription, Is.EqualTo("Warning"));
    }

    [Test]
    public void WithLogMessageUsingCallback_ReturnVoid()
    {
        var log = new Mock<ILog>();

        var counter = 10;

        log.Setup(x => x.WithLogMessageToDatabase(It.IsAny<string>()))
            .Callback(() => counter++)
            .Returns(true)
            .Callback(() => counter++);

        log.Object.WithLogMessageToDatabase("Hello World!");

        Assert.That(counter, Is.EqualTo(12));
    }

    [Test]
    public void WithLogMessageUsingVerify_ReturnVoid()
    {
        var log = new Mock<ILog>();

        log.Setup(x => x.WithLogMessageToConsole(It.IsAny<string>()));

        _bankAccount = new BankAccount(log.Object);

        _bankAccount.Deposit(100);

        Assert.That(_bankAccount.Balance.Amount, Is.EqualTo(100));

        log.Verify(x => x.WithLogMessageToConsole(It.IsAny<string>()), Times.Exactly(2));
        log.Verify(x => x.WithLogMessageToConsole("Test Verification"), Times.AtLeastOnce);
        log.VerifySet(x=>x.LogLevel = 102, Times.Once);
        log.VerifyGet(x=>x.LogLevel, Times.Once);
    }
}