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
            using(var context = new PersonenOrtContext())
            {
                var PersonToBeDeleted = context.Person.FirstOrDefault(p => p.Id == id);
                if (PersonToBeDeleted == null)
                    return "Person with id " + id + "not found";

                context.Person.Remove(PersonToBeDeleted);
                context.Person.Add(person);
                context.SaveChanges();
            }
            return "Person with id " + id + " has been updated";
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
                Ort? ort = context.Ort.FirstOrDefault(o => o.PLZ == person.Ort.PLZ);

                if(context.Ort.FirstOrDefault(o => o.PLZ == person.Ort.PLZ) != null)
                {
                    ort = new Ort(person.Ort.Name, person.Ort.PLZ);
                    context.Ort.Add(ort);
                }

                person.Ort = ort;
                context.Person.Add(person);  
                context.SaveChanges();
                var retval = new HttpResponseMessage();
                retval.StatusCode = System.Net.HttpStatusCode.OK;
                retval.RequestMessage = new HttpRequestMessage(HttpMethod.Post, "Add Person added to DB");
            }
            return person;
        }
    }
}