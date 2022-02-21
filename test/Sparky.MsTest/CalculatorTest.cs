using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sparky.MsTest;

[TestClass]
public class CalculatorTest
{
    [TestMethod]
    public void Sum_IntegerInput_ReturnCorrectOutput()
    {
        //Arrange
        //Act
        var result = Calculator.Sum(10, 20, 30);

        //Assert
        Assert.AreEqual(60, result);
    }
}