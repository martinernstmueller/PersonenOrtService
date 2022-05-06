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
        public IActionResult DeleteOrt(String plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(p => p.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return this.StatusCode(
                        StatusCodes.Status404NotFound,
                        "Ort with id " + plz + " not found");

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }

            return this.StatusCode(
                StatusCodes.Status501NotImplemented,
                "Ort with plz " + plz + "deleted");
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