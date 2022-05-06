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

        [HttpPut]
        public Person PutPerson(Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Person.Update(person);
                context.SaveChanges();
            }
            return person;
        }

        [HttpDelete("{id:int}")]
        public string DeletePerson(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.Include("Ort").FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return "Person with id " + id + "not found";

                string ortOfDeletedPerson = PersonToBeDeleted.Ort.PLZ;

                context.Person.Remove(PersonToBeDeleted);

                context.SaveChanges();

                if (context.Person.Include("Ort").ToList().FindAll(p => p.Ort.PLZ == ortOfDeletedPerson).Count == 0)
                {
                    context.Ort.Remove(PersonToBeDeleted.Ort);
                    context.SaveChanges();
                    return "removed Ort";
                }
            }
            return "Person with id " + id + "deleted";
        }

        [HttpPost(Name = "PostPerson")]
        public Person PostPerson(Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                Ort? dbOrt = context.Ort.FirstOrDefault(p => p.PLZ == person.Ort.PLZ);
                if (dbOrt == null)
                {
                    context.Ort.Add(new Ort(person.Ort.Name, person.Ort.PLZ));
                }
                person.Ort = dbOrt;
                context.Person.Add(person);
                context.SaveChanges();
            }
            return person;
        }
    }
}