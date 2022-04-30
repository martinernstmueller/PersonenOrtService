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
        public string PutOrt(string id, Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeUpdated = context.Ort.FirstOrDefault(p => p.PLZ == id);
                if (OrtToBeUpdated == null)
                    return "Ort with PLZ " + id + " not found";

                context.Ort.Remove(OrtToBeUpdated);
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return "Ort with PLZ " + id + " updated";
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(string id)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(p => p.PLZ == id);
                if (OrtToBeDeleted == null)
                    return "Ort with PLZ " + id + " not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Ort with PLZ " + id + " deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public Ort PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return ort;
        }
    }
}