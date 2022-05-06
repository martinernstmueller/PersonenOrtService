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
            return null;
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(int id)
        {
            return "deleted";
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
                    retval.Content = new StringContent("Add Ort with Plz" + ort.PLZ + " Failed");
                    return retval;
                }

                context.Ort.Add(ort);
                context.SaveChanges();

                retval.StatusCode = System.Net.HttpStatusCode.OK;
                retval.Content = new StringContent("Add Ort with Plz" + ort.PLZ + " to our Database");



                return retval;
            }
        }
    }
}