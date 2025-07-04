using ACME.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.SubtractService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtractController(ILogger<SubtractController> logger, ICalculator calculator) : ControllerBase
    {
        [HttpGet(Name = "Subtract")]
        public int Get(int a, int b)
        {
            logger.LogInformation("Subtracting {A} from {B}", b, a);
            return calculator.Subtract(a, b);
        }
    }
}
