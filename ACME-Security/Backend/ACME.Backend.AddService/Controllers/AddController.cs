using ACME.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.AddService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddController(ILogger<AddController> logger, ICalculator calculator) : ControllerBase
    {
        [HttpGet(Name = "Add")]
        public int Get(int a, int b)
        {
            logger.LogInformation("Adding {A} and {B}", a, b);
            return calculator.Add(a, b);
        }
    }
}
