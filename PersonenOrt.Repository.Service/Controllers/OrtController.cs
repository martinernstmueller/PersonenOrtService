using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;
using PersonenOrt.Repository.Service.Context;
using System.Net.Http;

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
        public String PutOrt(String plz, Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(o => o.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return "Ort with plz " + plz + "not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.Ort.Add(ort);
                context.SaveChanges();
            }
            return "Person with id " + plz + "deleted";
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(String plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(o => o.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return "Ort with plz " + plz + "not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Person with id " + plz + "deleted";
        }


        [HttpPost(Name = "PostOrt")]
        public Ort PostOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                var retval = new HttpResponseMessage();

                if (context.Ort.FirstOrDefault(o => o.PLZ == ort.PLZ) != null)
                {
                    retval.StatusCode = System.Net.HttpStatusCode.Conflict;
                    retval.Content = new StringContent("Add Ort wirht PLZ " + ort.PLZ + " to out Database failed");
                }
                context.Ort.Add(ort);
                context.SaveChanges();
                retval.StatusCode = System.Net.HttpStatusCode.OK;
                retval.RequestMessage = new HttpRequestMessage(HttpMethod.Post, "Add Ort wirht PLZ " + ort.PLZ + " to out Database"); 
            }
            return ort;
        }
    }
}