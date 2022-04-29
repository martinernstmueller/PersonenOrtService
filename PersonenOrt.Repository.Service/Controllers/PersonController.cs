using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;
using PersonOrt.Repository.Service.Context;

namespace PersonOrt.Repository.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        [HttpGet(Name = "GetPersons")]
        public IEnumerable<Person> Get()
        {
            using (var context = new PersonenOrtContext())
            {
                return context.Person.ToList();
            }
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


        [HttpPut("{id:int}")]
        public Person PutPerson(int id, Person person)
        {
            return null;
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
            return "Person with id " + id + " deleted";
        }
    }
}
