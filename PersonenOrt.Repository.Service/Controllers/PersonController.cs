using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonenOrt.Framework;
using PersonenOrt.Repository.Service.Context;

namespace PersonenOrt.Repository.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPersons")]
        public IEnumerable<Person> Get()
        {
            using (var context = new PersonenOrtContext())
            {
                return context.Person.Include(p => p.Ort).ToList();
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult PutPerson(int id, Person person)
        {
            return this.StatusCode(
                     StatusCodes.Status200OK,
                     "Person with " + id + " has been Changed");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePerson(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return this.StatusCode(
                   StatusCodes.Status404NotFound,
                   "Person with " + id + " Not found");

                context.Person.Remove(PersonToBeDeleted);
                context.SaveChanges();
            }
            return this.StatusCode(
                    StatusCodes.Status409Conflict,
                    "Person with " + id + " has been deleted");
        }


        [HttpPost(Name = "PostPerson")]
        public IActionResult PostPerson(Person person)
        {
            var retval = new HttpResponseMessage();
            using (var context = new PersonenOrtContext())
            {
                Ort? ort = context.Ort.FirstOrDefault(o => o.PLZ == person.Ort.PLZ);
                if (ort == null)
                {
                    ort = new Ort(person.Ort.Name, person.Ort.PLZ);
                    context.Ort.Add(ort); 
                }
                person.Ort = ort;
                context.Person.Add(person);
                context.SaveChanges();
                return this.StatusCode(
                    StatusCodes.Status200OK, 
                    "Add Person with Name " + person.Name + " succeeded.");
            }
        }
    }
}