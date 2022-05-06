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
        public HttpResponseMessage PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var retval = new HttpResponseMessage();

                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) !=null)
                {
                    retval.StatusCode = System.Net.HttpStatusCode.Conflict;
                    retval.Content = new StringContent("Add Ort with PLZ " + ort.PLZ + " failed! PLZ already exists");
                    return retval;
                }
                context.Ort.Add(ort);
                context.SaveChanges();
                retval.Content = new StringContent("Add Ort with PLZ " + ort.PLZ + " to our Databse");
                return retval;
            }
        }
    }
}