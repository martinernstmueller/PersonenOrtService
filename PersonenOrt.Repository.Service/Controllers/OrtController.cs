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

        [HttpGet, Route("OrtGet")]
        public IEnumerable<Ort> Get()
        {
            using (var context = new PersonenOrtContext())
            {
                return context.Ort.ToList();
            }
        }

        

        [HttpPost, Route("OrtPost")]
        public Ort OrtPost(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return ort;
        }

        [HttpPut, Route("OrtPut")]
        public String PutOrt(string name, Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeUpdated = context.Ort.FirstOrDefault(x => x.Name == name);
                if (OrtToBeUpdated == null)
                {
                    throw new Exception();
                }

                context.Ort.Remove(OrtToBeUpdated);
                context.Ort.Add(ort);
                context.SaveChanges();

                return "Ort has been updated!";
            }
        }

        [HttpDelete, Route("OrtDelete")]
        public string DeleteOrt(string name)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(p => p.Name == name);
                if (OrtToBeDeleted == null)
                    return "Ort with Name " + name + " not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Ort with Name " + name + " deleted";
        }
    }
}