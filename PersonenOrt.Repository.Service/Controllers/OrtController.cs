using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;
using PersonenOrt.Repository.Service.Context;

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
            using (var context = new PersonenOrtContext())
            {
                return context.Ort.ToList();
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult PutOrt(int id, Ort ort)
        {
            return Ok("update");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrt(int id)
        {
            return Ok("deleted");
        }


        [HttpPost(Name = "PostOrt")]
        public IActionResult PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) != null)
                {
                    return Problem(detail: ("Add Ort with PLZ " + ort.PLZ + " failed! PLZ already exists."));
                }

                context.Ort.Add(ort);
                context.SaveChanges();
                return Ok("Add Ort with PLZ " + ort.PLZ + " to out Database");
            }
        }
    }
}