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
            return this.StatusCode(
                StatusCodes.Status501NotImplemented,
                "Put of Ort needs to be implemented.");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrt(int id)
        {
            return this.StatusCode(
                StatusCodes.Status501NotImplemented,
                "Delete of Ort needs to be implemented.");

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