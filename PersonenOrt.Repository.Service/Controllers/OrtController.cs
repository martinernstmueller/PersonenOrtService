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
        public IEnumerable<Ort> GetOrts()
        {
            using (var context = new PersonenOrtContext())
            {
                return context.Ort.ToList();
            }
        }

        [HttpPut(Name = "PutOrt")]
        public String PutOrt(String Name, Ort ort)
        {

            using (var context = new PersonenOrtContext())
            {
                var OrtToBeUpdated = context.Ort.FirstOrDefault(p => p.Name == Name);
                if (OrtToBeUpdated == null)
                    return "Ort with name " + Name + "not found";
                context.Ort.Remove(OrtToBeUpdated);
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return "Ort with name " + Name + "updated";
        }

        [HttpDelete(Name = "DeleteOrt")]
        public String DeleteOrt(String Name)
        {

            using (var context = new PersonenOrtContext())
            {
                var OrtToBeUpdated = context.Ort.FirstOrDefault(p => p.Name == Name);
                if (OrtToBeUpdated == null)
                    return "Ort with name " + Name + "not found";
                context.Ort.Remove(OrtToBeUpdated);
                context.SaveChanges();
            }
            return "Ort with name " + Name + " removed";
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