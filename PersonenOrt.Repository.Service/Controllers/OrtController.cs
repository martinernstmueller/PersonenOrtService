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

        [HttpPut("{plz}")]
        public IActionResult PutOrt(String plz, Ort ort)
        {
            if (plz != ort.PLZ && ort.PLZ != null)
            {
                return Conflict("PLZ in query differs from PLZ in path");
            }
            using (var context = new PersonenOrtContext())
            {
                Ort? ortDB = context.Ort.FirstOrDefault(o => o.PLZ == plz);
                if (ortDB == null)
                {
                    return Conflict("Plz " + plz + " not found in Database");
                }
                ortDB.Name = ort.Name;
                context.SaveChanges();
                return Ok("Ort with plz " + plz + " changed");
            }
        }

        [HttpDelete("{plz}")]
        public IActionResult DeleteOrt(string plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(p => p.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return Problem(detail: "this Ort doesn't exist");
                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }

            return Ok("Ort with plz " + plz + " deleted");
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
                return Ok("Added Ort with PLZ " + ort.PLZ + " to our Database");
            }
        }
    }
}