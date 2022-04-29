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
                var PersonToBeUpdated = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeUpdated == null)
                    return "Person with id " + id + "not found";
                context.Person.Remove(PersonToBeUpdated);
                context.Person.Add(person);
                context.SaveChanges();
            }
            return "Person with id " + id + "updated";
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

                int indicator = 0;
                foreach (var person in context.Person)
                    if (person.Ort == PersonToBeDeleted.Ort)
                        indicator++;

                if (indicator == 0)
                    context.Ort.Remove(PersonToBeDeleted.Ort);
                                                                    
            };
            return "Person with id " + id + "deleted";
        }


        [HttpPost(Name = "PostPerson")]
        public Person PostPerson(Person person1)
        {
            using (var context = new PersonenOrtContext())
            {
                var ort = context.Ort.FirstOrDefault(ort => person1.Ort.PLZ == ort.PLZ);
                if (ort == null)
                {
                    return null;
                }
                person1.Ort = ort;
                context.Person.Add(person1);
                context.SaveChanges();

            }
            return person1;
        }
    }
}