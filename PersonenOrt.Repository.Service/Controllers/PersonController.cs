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

        [HttpPut("{id}")]
        public IActionResult Put(int id, Person person)
        {
            if (id != person.Id && person.Id != null)
            {
                return Conflict("ID in query differs from ID in path");
            }
            using (var context = new PersonenOrtContext())
            {
                Person? personDB = context.Person.FirstOrDefault(p => p.Id == id);
                if (personDB == null)
                {
                    return Conflict("ID " + id + " not found in Database");
                }
                personDB.Name = person.Name;
                personDB.Vorname = person.Vorname;
                //alles funktioniert, außer das Updaten des Ortes von der Person
                //wenn der Ort nicht exestiert wird er erstellt
                //wenn er exestiert kommt eine Exception
                //personDB.Ort = person.Ort;
                //personDB.Ort.Name = person.Ort.Name;
                //personDB.Ort.PLZ = person.Ort.PLZ;
                personDB.Geburtsdatum = person.Geburtsdatum;
                context.SaveChanges();
                return Ok(personDB);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePerson(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return Problem(detail: "this Person doesn't exist");
                context.Person.Remove(PersonToBeDeleted);
                context.SaveChanges();
            }

            return Ok("Person with id " + id + "deleted");
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
                retval.StatusCode = System.Net.HttpStatusCode.OK;
                retval.Content = new StringContent("Add Person with Name " + person.Name + " succeeded.");
                return Ok(person);
            }
        }
    }
}