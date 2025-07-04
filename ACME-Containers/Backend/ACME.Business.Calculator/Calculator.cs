using ACME.Business.Interfaces;

namespace ACME.Business;

public class Calculator : ICalculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
