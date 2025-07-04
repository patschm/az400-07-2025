using ACME.Web.Calculator.Models;
using System.Collections;

namespace ACME.Tests.Integration;

public class CalculatorModelGenerator : IEnumerable<object[]>
{
    public static IEnumerable<object[]> GetModels()
    {
        yield return [new CalculatorModel { A = 10, B = 20, Result = 30 }];
        yield return [new CalculatorModel { A = 1, B = 2, Result = 3 }];
        yield return [new CalculatorModel { A = 14, B = 32, Result = 46 }];
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
