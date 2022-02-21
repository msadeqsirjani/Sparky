using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class CustomerTest
{
    [Fact]
    public void Customer_CreateCustomerWithLessThan100Order_ReturnRegularCustomer()
    {
        var customer = new Customer("Mohammad Sadeq", "Sirjani", 56m);

        customer.GetCustomer().Should().BeOfType<Customer.RegularCustomer>();
        customer.GetCustomer().Should().BeOfType(typeof(Customer.RegularCustomer));
    }
}