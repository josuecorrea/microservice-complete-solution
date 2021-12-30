using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Microservice.ApiGateway.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _log;

        public GatewayController(ILogger<GatewayController> log)
        {
            _log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            _log.LogInformation("Request status verifications");

            return Ok(await Task.FromResult("Online"));
        }
    }
}
