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
        [Route("PersonContainingStringInName/{searchString}")]
        public List<Person> PersonContainingStringInName(string searchString)
        {
            return PersonHelper.GetPersonsContainingStringInName(searchString, PersonHelper.Persons);
        }


        [HttpGet]
        [Route("PersonNameLetterCount")]
        public double PersonNameLetterCount()
        {
            return PersonHelper.GetMeanAge(PersonHelper.Persons);
        }

        [HttpGet]
        [Route("PersonsContainsString/{searchTerm}")]
        public List<Person> PersonsContainsString(string searchTerm)
        {
            return PersonHelper.GetPersonsContainingStringInName(searchTerm, PersonHelper.Persons);
        }


        [HttpGet]
        [Route("PersonsWithPLZ/{PLZ}")]
        public List<Person> PersonsWithPLZ(int PLZ)
        {
            return PersonHelper.GetPersonsWithPLZ_LINQ(PLZ, PersonHelper.Persons);
        }

        [HttpDelete]
        [Route("DeletePerson/{personId}")]
        public void DeletePerson(int personId)
        {
            var personToBeRemoved = PersonHelper.Persons.FirstOrDefault(p => p.Id == personId);
            if (personToBeRemoved != null)
              PersonHelper.Persons.Remove(personToBeRemoved);
        }

        [HttpPut]
        public void PostPerson(Person person)
        {
            PersonHelper.Persons.Add(person); 
        }
        
    }
}