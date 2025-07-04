using ACME.Business;
using Xunit;

namespace ACME.Tests.Unit;

public class CalculatorTests
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-4, 5, 1)]
    [InlineData(-3, -2, -5)]
    public void TestAdd(int a, int b, int expected)
    {
        Calculator calc = new Calculator();
        int actual = calc.Add(a, b);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 2, -1)]
    [InlineData(-4, 5, -9)]
    [InlineData(-3, -2, -1)]
    public void TestSubtract(int a, int b, int expected)
    {
        Calculator calc = new Calculator();
        int actual = calc.Subtract(a, b);
        Assert.Equal(expected, actual);
    }
}
