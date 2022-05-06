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
                return Conflict("PLZ in query differs from PLZ in path");
          
            using (var context = new PersonenOrtContext())
            {
                Ort? ortDB = context.Ort.FirstOrDefault(o => o.PLZ == PLZ);
                if (ortDB == null)
                    return Conflict("PLZ " + PLZ + "not found in Database");

                ortDB.Name = ort.Name;
                context.SaveChanges();

                return Ok(ortDB);
            }

            return this.StatusCode(
                    StatusCodes.Status200OK,
                    "ort with " + PLZ + " has been Changed");
        }

        [HttpDelete("{PLZ}")]
        public IActionResult DeleteOrt(string PLZ)
        {
            return this.StatusCode(
                    StatusCodes.Status409Conflict,
                    "ort with " + PLZ + " has been deleted");
        }


        [HttpPost(Name = "PostOrt")]
        public IActionResult PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var retval = new HttpResponseMessage();

                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) != null)
                {
                    return this.StatusCode(
                        StatusCodes.Status409Conflict,
                        "Add Ort with PLZ " + ort.PLZ + " failed! PLZ already exists.");
                }
                context.Ort.Add(ort);
                context.SaveChanges();
                return this.StatusCode(
                    StatusCodes.Status200OK,
                     "Add Ort with PLZ " + ort.PLZ + " to out Database");
            }
        }
    }
}