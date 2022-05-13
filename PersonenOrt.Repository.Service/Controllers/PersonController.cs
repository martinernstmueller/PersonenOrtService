using Microsoft.AspNetCore.Mvc;
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
                return context.Person.ToList();
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult PutPerson(int id, Person person)
        {
            if (person.Id != null)
                if (id != person.Id)
                    return Conflict("I in query differs from ID in path");

            using (var context = new PersonenOrtContext())
            {
                Person? personDB = context.Person.FirstOrDefault(o => o.Id == id);
                if (personDB == null)
                    return Conflict("ID " + id + " not found in Database");
                personDB.Geburtsdatum = person.Geburtsdatum;
                personDB.Name = person.Name;
                personDB.Vorname = person.Vorname;
                person.Ort.PLZ = person.Ort.PLZ;
                person.Ort.Name = person.Ort.Name;
                context.SaveChanges();
                return Ok(personDB);
            }
        }

        [HttpDelete("{id:int}")]
        public string DeletePerson(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return "Person with id " + id + "not found";

                context.Person.Remove(PersonToBeDeleted);
                context.SaveChanges();
            }
            return "Person with id " + id + "deleted";
        }


        [HttpPost(Name = "PostPerson")]
        public Person PostPerson(Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Person.Add(person);
                context.SaveChanges();
            }
            return person;
        }
    }
}