namespace Sparky;

public interface ILog
{
    int LogLevel { get; set; }
    string? LogLevelDescription { get; set; }
    void WithLogMessageToConsole(string message);
    bool WithLogMessageToDatabase(string message);
    bool WithLogMessageAfterWithdraw(Money money);
    string WithLogMessageReturnString(string message);
    bool WithLogMessageReturnBoolean(string message, out string? value);
    bool WithLogMessageWithRef(ref Customer customer);
}

public class Log : ILog
{
    public int LogLevel { get; set; }
    public string? LogLevelDescription { get; set; }
    public void WithLogMessageToConsole(string message) => Console.WriteLine(message);
    public bool WithLogMessageToDatabase(string message)
    {
        Console.WriteLine(message);

        return true;
    }
    public bool WithLogMessageAfterWithdraw(Money money)
    {
        bool result;
        if (money.Amount >= 0)
        {
            Console.WriteLine("Success");

            result = true;
        }
        else
        {
            Console.WriteLine("Failure");

            result = false;
        }

        return result;
    }
    public string WithLogMessageReturnString(string message) => message.ToLowerInvariant();
    public bool WithLogMessageReturnBoolean(string message, out string? value)
    {
        value = $"Hello {message}";

        return true;
    }
    public bool WithLogMessageWithRef(ref Customer customer) => true;
}

public class FakeLog : ILog
{
    public int LogLevel { get; set; }
    public string? LogLevelDescription { get; set; }

    public void WithLogMessageToConsole(string message)
    {
        
    }
    public bool WithLogMessageToDatabase(string message) => true;
    public bool WithLogMessageAfterWithdraw(Money money) => true;
    public string WithLogMessageReturnString(string message) => string.Empty;
    public bool WithLogMessageReturnBoolean(string message, out string? value)
    {
        value = null;
        return true;
    }
    public bool WithLogMessageWithRef(ref Customer customer) => true;
}