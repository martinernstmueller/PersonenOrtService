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
        public String PutPerson(int id, Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeUpdated = context.Person.FirstOrDefault(x => x.Id == id);
                if (PersonToBeUpdated == null)
                {
                    throw new Exception();
                }

                context.Person.Remove(PersonToBeUpdated);
                context.Person.Add(person);
                context.SaveChanges();

                return "Person has been updated!";
            }
        }

        [HttpPost, Route("PersonPost")]
        public Person PersonPost(Person person)
        {
            using (var context = new PersonenOrtContext())
            {

                    context.Person.Add(person);

                    context.SaveChanges();
                
            }
            return person;
        }

        [HttpDelete("{id:int}")]
        public string DeletePerson(int id)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return "Person with id " + id + " not found";

                context.Person.Remove(PersonToBeDeleted);
                context.SaveChanges();
            }
            return "Person with id " + id + "deleted";
        }
    }
}