using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;

namespace PersonenOrt.Repository.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrtController : ControllerBase
    {
        private readonly ILogger<OrtController> _logger;

        public OrtController(ILogger<OrtController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOrts")]
        public IEnumerable<Ort> Get()
        {
            return new List<Ort>();
        }

        [HttpPut("{id:int}")]
        public Person PutOrt(int id, Ort ort)
        {
            return null;
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(int id)
        {
            return "deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public Person PostOrt(int id)
        {
            return null;
        }
    }
}