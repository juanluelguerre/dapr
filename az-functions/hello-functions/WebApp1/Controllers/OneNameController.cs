using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OneNameController : ControllerBase
    {
        private readonly ILogger<OneNameController> _logger;

        public OneNameController(ILogger<OneNameController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Post([FromBody] string name)
        {
            _logger.LogInformation($"Service WebApp1 called for '{name}'");            
        }
    }
}
