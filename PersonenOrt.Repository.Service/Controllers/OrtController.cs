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
        public Person PutOrt(int id, Ort ort)
        {
            return null;
        }
        
        [HttpDelete]
        public string DeleteOrt(string plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(o => o.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return "Ort with id " + plz + " not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Ort with id " + plz + " deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public HttpResponseMessage PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var retval = new HttpResponseMessage();

                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) != null)
                {
                    retval.StatusCode = System.Net.HttpStatusCode.Conflict;
                    retval.Content = new StringContent("Add Ort with PLZ " + ort.PLZ + " failed! PLZ already exists.");
                    return retval;
                }
                    
                context.Ort.Add(ort);
                context.SaveChanges();
                retval.Content = new StringContent("Add Ort with PLZ " + ort.PLZ + " to out Database");
                return retval;
            }
        }

    }
}