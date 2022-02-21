namespace Sparky;

public record Person(string Forename, string Surname)
{
    public string Fullname
    {
        get
        {
            Discount = 25;

            if (Forename.IsNullOrEmpty())
                throw new ArgumentException($"{nameof(Forename)} is null");

            return $"{Forename} {Surname}";
        }
    }

    public string? Nothing => null;

    public int Discount { get; private set; } = 15;
}