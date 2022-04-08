using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;

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
            return new List<Person>();
        }

        [HttpPut("{id:int}")]
        public Person PutPerson(int id, Person person)
        {
            return null;
        }

        [HttpDelete("{id:int}")]
        public string DeletePerson(int id)
        {
            return "deleted";
        }


        [HttpPost(Name = "PostPerson")]
        public Person PostPerson(int id)
        {
            return null;
        }
    }
}