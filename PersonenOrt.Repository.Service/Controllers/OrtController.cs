using System.Linq;
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
        public Person PutOrt(int id, Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeUpdated = context.Ort.FirstOrDefault(o => o.PLZ == id);
                if (OrtToBeUpdated == null)
                    return null;
                OrtToBeUpdated.Name = ort.Name;
                OrtToBeUpdated.PLZ = ort.PLZ;
                context.Ort.Update(OrtToBeUpdated);
                context.SaveChanges();
            }
            return null;
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(o => o.PLZ == id);
                if (OrtToBeDeleted == null)
                    return "Ort nicht gefunden";
                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public Ort PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeCreated = context.Ort.FirstOrDefault(o => o.PLZ.Equals(ort.PLZ));
                if (OrtToBeCreated != null)
                    return null;
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return ort;
        }
    }
}