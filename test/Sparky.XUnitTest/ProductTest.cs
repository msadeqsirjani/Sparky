using System;
using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class ProductTest
{
    [Fact]
    public void GetPrice_PremiumCustomer_ReturnPriceWith20Discount()
    {
        Product product = new(Guid.NewGuid(), "Car", 50000m);
        Customer customer = new("Mohammad Sadeq", "Sirjani", 100);

        var price = product.GetPrice(customer);

        price.Should().Be(40000m);
    }
}