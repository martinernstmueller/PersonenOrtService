using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;
using PersonOrt.Repository.Service.Context;

namespace PersonOrt.Repository.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrtController : ControllerBase
    {

        [HttpGet(Name = "GetOrts")]
        public IEnumerable<Ort> Get()
        {
            using (var context = new PersonenOrtContext())
            {
                return context.Ort.ToList();
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


        [HttpPut("{id:int}")]
        public Ort PutOrt(int id, Ort ort)
        {
            return null;
        }


        [HttpDelete("{id:int}")]
        public string DeleteOrt(String plz)
        {
            using (var context = new PersonenOrtContext())
            {
                var OrtToBeDeleted = context.Ort.FirstOrDefault(o => o.PLZ == plz);
                if (OrtToBeDeleted == null)
                    return "Ort with plz " + plz + " not found";

                context.Ort.Remove(OrtToBeDeleted);
                context.SaveChanges();
            }
            return "Person with id " + plz + " deleted";
        }
    }
}