using ACME.Web.Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace ACME.Web.Calculator.Controllers;

public class CalculatorController(IHttpClientFactory httpClientFactory, IFeatureManager man) : Controller
{
    
    public async Task<IActionResult> Index(CalculatorModel? model = null)
    {
        if (await man.IsEnabledAsync("subtract"))
        {

        }
        if (model == null) model = new CalculatorModel();
        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CalculatorModel model)
    {
        var client = httpClientFactory.CreateClient("AddService");
        var result = await client.GetAsync($"?a={model.A}&b={model.B}");
        if (result.IsSuccessStatusCode)
        {
            model.Result = await result.Content.ReadFromJsonAsync<int>();
        }
        
        return View("Index", model);
    }

    [FeatureGate("subtract")]
    [HttpPost]
    public async Task<IActionResult> Subtract(CalculatorModel model)
    {
        var client = httpClientFactory.CreateClient("SubtractService");
        var result = await client.GetAsync($"?a={model.A}&b={model.B}");
        if (result.IsSuccessStatusCode)
        {
            model.Result = await result.Content.ReadFromJsonAsync<int>();
        }

        return View("Index", model);
    }
}
