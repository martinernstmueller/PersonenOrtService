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

        [HttpPut("{PLZ}")]
        public IActionResult PutOrt(string PLZ, Ort ort)
        {
            if (PLZ != ort.PLZ && ort.PLZ != null)
            {
                return Ok("hallo");
            }
            ort.PLZ = PLZ;
            using (var context = new PersonenOrtContext())
            {
                Ort dbOrt = context.Ort.FirstOrDefault(o => o.PLZ == PLZ);
                if (dbOrt == null)
                    return Conflict("fehler existiert nicht");
                dbOrt.Name = ort.Name;
                context.SaveChanges();
                return Ok(ort);
            }
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
                    return Conflict(error: "Add Ort with PLZ " + ort.PLZ + " failed! PLZ already exists.");
                }

                context.Ort.Add(ort);
                context.SaveChanges();
                return Ok("Add Ort with PLZ " + ort.PLZ + " to out Database");
            }
        }
    }
}