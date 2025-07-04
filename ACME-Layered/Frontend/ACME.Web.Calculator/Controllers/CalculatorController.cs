using ACME.Business.Interfaces;
using ACME.Web.Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Web.Calculator.Controllers;

public class CalculatorController(ICalculator calculator) : Controller
{
    public IActionResult Index(CalculatorModel? model = null)
    {
        if (model == null) model = new CalculatorModel();
        return View("Index", model);
    }

    [HttpPost]
    public IActionResult Add(CalculatorModel model)
    {
        model.Result = calculator.Add(model.A, model.B);
        return View("Index", model);
    }
}
