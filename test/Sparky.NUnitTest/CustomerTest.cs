using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class CustomerTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Customer_CreateCustomerWithLessThan100Order_ReturnRegularCustomer()
    {
        var customer = new Customer("Mohammad Sadeq", "Sirjani", 56m);

        Assert.Multiple(() =>
        {
            Assert.IsInstanceOf<Customer.RegularCustomer>(customer.GetCustomer());
            Assert.That(customer.GetCustomer(), Is.TypeOf<Customer.RegularCustomer>());
        });
    }
}