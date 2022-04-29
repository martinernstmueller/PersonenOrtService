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
        public Person PutPerson(Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                context.Person.Update(person);
                context.SaveChanges();
                return person;
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
                var persort = context.Person.FirstOrDefault(a => a.Ort == person.Ort);
                if (persort == null)
                {

                }
                context.Person.Add(person);
                context.SaveChanges();
            }
            return person;
        }
    }
}