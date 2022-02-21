namespace Sparky;

public record Product(Guid Id, string Title, decimal Price)
{
    public decimal GetPrice(Customer customer) => customer.IsPremium ? Price * 0.8.ToDecimal() : Price;
}