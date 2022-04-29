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
        public Person PutPerson(int id, Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                var PersonToBeUpdated = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeUpdated == null)
                    return null;
                context.Person.Remove(PersonToBeUpdated);
                context.SaveChanges();
                PostPerson(PersonToBeUpdated);
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
                    return "Person with id " + id + "not found";

                var ort = context.Ort.FirstOrDefault(ort => PersonToBeDeleted.Ort.PLZ == ort.PLZ);
                if (ort == null)
                {
                    return null;
                }
                PersonToBeDeleted.Ort = ort;
                
                context.Person.Remove(PersonToBeDeleted);
                context.SaveChanges();

                if (!context.Person.Any(p => p.Ort == PersonToBeDeleted.Ort))
                {
                    context.Ort.Remove(PersonToBeDeleted.Ort);
                }
            }
            return "Person with id " + id + "deleted";
        }


        [HttpPost(Name = "PostPerson")]
        public Person PostPerson(Person person)
        {
            using (var context = new PersonenOrtContext())
            {
                var ort = context.Ort.FirstOrDefault(ort => person.Ort.PLZ == ort.PLZ);
                if (ort == null)
                {
                    return null;
                }
                person.Ort = ort;
                context.Person.Add(person);
                context.SaveChanges();
            }
            return person;
        }
    }
}