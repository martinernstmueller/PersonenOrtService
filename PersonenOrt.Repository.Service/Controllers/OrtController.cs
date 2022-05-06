using System.Net;
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

        [HttpPut]
        public Ort PutOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Ort.Update(ort);
                context.SaveChanges();
            }
            return ort;
        }

        [HttpDelete("{plz:int}")]
        public string DeleteOrt(int plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(p => p.PLZ == plz.ToString());
                if (OrtToBeDeleted == null)
                    return "Ort with plz " + plz + "not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Person with plz " + plz + "deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public ActionResult PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {

                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) != null)
                {
                    return Problem(detail:"hallo");
                }
                context.Ort.Add(ort);
                context.SaveChanges();
                var retval = new HttpResponseMessage();
                retval.StatusCode = HttpStatusCode.OK;
                retval.Content = new StringContent("Added Ort with PLZ " + ort.PLZ);
                return Ok(ort); 
            }
        }
    }
}