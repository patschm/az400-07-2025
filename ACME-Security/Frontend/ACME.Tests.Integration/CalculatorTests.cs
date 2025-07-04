
using ACME.Web.Calculator;
using ACME.Web.Calculator.Models;

namespace ACME.Tests.Integration;

public class CalculatorTests: IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public CalculatorTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

   // [Fact]
    public async Task TestIndexWithoutModel()
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync("/calculator/index");

        Assert.NotNull(respose);
        Assert.True(respose.IsSuccessStatusCode);
    }

   // [Theory]
   // [MemberData(nameof(CalculatorModelGenerator.GetModels), MemberType =typeof(CalculatorModelGenerator))]
    public async Task TestIndexWithModel(CalculatorModel model)
    {
        var client = _factory.CreateClient();
        var data = new Dictionary<string, string>();
        data.Add(nameof(model.A), model.A.ToString());
        data.Add(nameof(model.B), model.B.ToString());
        data.Add(nameof(model.Result), model.Result.ToString());
        var content = new FormUrlEncodedContent(data);
        
        var respose = await client.PostAsync("/calculator/Add", content);

        Assert.NotNull(respose);
        Assert.True(respose.IsSuccessStatusCode);
        var retStr = await respose.Content.ReadAsStringAsync();
        Assert.Contains($"<h1>{model.Result}</h1>", retStr);
    }
}
