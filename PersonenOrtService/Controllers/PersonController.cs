using Microsoft.AspNetCore.Mvc;
using PersonenOrt.Framework;

namespace PersonenOrtService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonenController : ControllerBase
    {
        private readonly ILogger<PersonenController> _logger;
        public PersonenController(ILogger<PersonenController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPersons")]
        public IEnumerable<Person> Get()
        {
            int summeAlter = 0;
            foreach (var person in PersonHelper.Persons)
            {
                summeAlter += DateTime.Now.Year -  person.Alter;
            }

            _logger.LogDebug("Get all persons...");
            return PersonHelper.Persons.ToArray();
        }

        [HttpGet]
        [Route("PersonGetMeanAge")]
        public double PersonMeanAge()
        {
            return PersonHelper.GetMeanAge(PersonHelper.Persons);
        }


        [HttpGet]
        [Route("PersonNameLetterCount")]
        public double PersonNameLetterCount()
        {
            return PersonHelper.GetMeanAge(PersonHelper.Persons);
        }
    }
}