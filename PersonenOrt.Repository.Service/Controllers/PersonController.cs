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
            if (id != person.Id && person.Id != 0)
            {
                return Conflict("Id in query differs from Id in path");
            }
            using (var context = new PersonenOrtContext())
            {
                
                Person? personDB = context.Person.FirstOrDefault(p => p.Id == id);
                if (personDB == null)
                { 
                    return Conflict("Id " + id + " not found in Database"); 
                }

                personDB.Id = person.Id;
                personDB.Name = person.Name;
                personDB.Vorname = person.Vorname;
                personDB.Ort = person.Ort;
                personDB.Geburtsdatum = person.Geburtsdatum;

                Ort? ortDB = context.Ort.FirstOrDefault(o => o.PLZ == person.Ort.PLZ);
                if (ortDB == null)
                {
                    ortDB = new Ort(person.Ort.PLZ, person.Ort.Name);
                    context.Ort.Add(ortDB);
                }

                personDB.Ort = ortDB;;

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