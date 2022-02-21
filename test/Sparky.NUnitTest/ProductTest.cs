using System;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class ProductTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void GetPrice_PremiumCustomer_ReturnPriceWith20Discount()
    {
        Product product = new(Guid.NewGuid(), "Car", 50000m);
        Customer customer = new("Mohammad Sadeq", "Sirjani", 100);

        var price = product.GetPrice(customer);

        Assert.That(price, Is.EqualTo(40000m));
    }
}