using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;

namespace PersonenOrtService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrtController : ControllerBase
    {
        private readonly ILogger<PersonenController> _logger;
        public OrtController(ILogger<PersonenController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOrts")]
        public IEnumerable<Ort> Get()
        {
            _logger.LogDebug("Get all orts...");
            return PersonHelper.Orts.ToArray();
        }

      
    }
}