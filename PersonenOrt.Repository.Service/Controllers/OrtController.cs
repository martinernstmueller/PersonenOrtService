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

        [HttpPut(Name = "UpdateOrte")]
        public Ort PutOrt(Ort ort)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Ort.Update(ort);
                context.SaveChanges();
                return ort;
            }
        }

        [HttpDelete("{id:int}")]
        public string DeleteOrt(String id)
        {
            using (var context = new PersonenOrtContext())
            {
                Ort ort = context.Ort.FirstOrDefault(p => p.PLZ == id);
                if (ort == null)
                    return "Ort existiert nicht";
                context.Ort.Remove(ort);
                context.SaveChanges();
                return ort.Name + " deleted";
            }
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