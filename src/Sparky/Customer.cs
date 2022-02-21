namespace Sparky;

public record Customer(string Forename, string Surname, decimal Order)
{
    public bool IsPremium => GetCustomer() is PremiumCustomer;

    public Customer GetCustomer()
    {
        return Order >= 100
            ? new PremiumCustomer(Forename, Surname, Order)
            : new RegularCustomer(Forename, Surname, Order);
    }

    public record RegularCustomer(string Forename, string Surname, decimal Order) : Customer(Forename, Surname, Order);

    public record PremiumCustomer(string Forename, string Surname, decimal Order) : Customer(Forename, Surname, Order);
}