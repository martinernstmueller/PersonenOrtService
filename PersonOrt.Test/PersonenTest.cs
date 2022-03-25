using NUnit.Framework;
using PersonenOrt.Framework;
using System;
using System.Collections.Generic;

namespace PersonOrt.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetMeanAge_TwoPersons_MeanAgeOfPersons()
        {
            List<Person> Persons = new List<Person>()
            {
                new Person() { Geburtsdatum = new DateTime(2001, 6, 5)},
                new Person() { Geburtsdatum= new DateTime(2000, 1, 4)}
            };
            var meanAge = PersonHelper.GetMeanAge(Persons);
            Assert.IsTrue(meanAge == 21);
        }

    
    }
}