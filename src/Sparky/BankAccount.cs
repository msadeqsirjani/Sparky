namespace Sparky;

public class BankAccount
{

    private readonly ILog _log;

    public Money Balance { get; private set; }

    public BankAccount(ILog log)
    {
        _log = log;
        Balance = new Money(0);
    }

    public bool Deposit(double amount)
    {
        _log.WithLogMessageToConsole($"{nameof(Deposit)} Invoked");
        _log.WithLogMessageToConsole("Test Verification");

        _log.LogLevel = 102;
        _log.LogLevelDescription = _log.LogLevel.ToString();

        Balance = new Money(Balance.Amount + amount);

        return true;
    }

    public bool WithDraw(double amount)
    {
        _log.WithLogMessageToConsole($"{nameof(WithDraw)} Invoked");

        if (Balance.Amount < amount)
            return _log.WithLogMessageAfterWithdraw(new Money(Balance.Amount - amount));

        Balance = new Money(Balance.Amount - amount);

        return _log.WithLogMessageAfterWithdraw(Balance);
    }
}

public record Money(double Amount) : IComparable
{
    public int CompareTo(object? obj)
    {
        if (obj is Money other)
        {
            return Amount.CompareTo(other.Amount);
        }

        return 1;
    }
}