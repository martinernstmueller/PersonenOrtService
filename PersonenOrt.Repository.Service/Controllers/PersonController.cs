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
                return BadRequest("id in path and person don't match");
            }

            using (var context = new PersonenOrtContext())
            {
                if (!context.Person.Any(person1 => person1.Id == id))
                {
                    return BadRequest("Person can't be found");
                }

                var personToBeUpdated = context.Person.FirstOrDefault(p => p.Id == id);
                if (personToBeUpdated == null)
                    return StatusCode(500, "Person can't be found");
                Ort? ort = context.Ort.FirstOrDefault(o => o.PLZ == person.Ort.PLZ);
                if (ort == null)
                {
                    ort = new Ort(person.Ort.Name, person.Ort.PLZ);
                    context.Ort.Add(ort);
                }
                person.Ort = ort;
                personToBeUpdated.Name = person.Name;
                personToBeUpdated.Vorname = person.Vorname;
                personToBeUpdated.Geburtsdatum = person.Geburtsdatum;
                personToBeUpdated.Id = person.Id;
                personToBeUpdated.Ort = person.Ort;
                context.Person.Update(personToBeUpdated);
                context.SaveChanges();
            }
            return Ok("update");
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