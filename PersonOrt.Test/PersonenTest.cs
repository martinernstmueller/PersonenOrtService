using NUnit.Framework;
using PersonenOrt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [Test]
        public void CountLettersImplementaions()
        {
            List<Person> Persons = new List<Person>()
            {
                new Person() { Name = "aaa", Vorname = "aaa" },
                new Person() { Name = "bb", Vorname = "bb"}
            };

            var letters = PersonHelper.CountLettersOfName(Persons);
            Assert.IsTrue(letters['a'] == 3);
            Assert.IsTrue(letters['b'] == 2);
        }

        [Test]
        public void GetPersons_ContainingString_RetrunsPersons()
        {
            var person1 = new Person() { Name = "aaa", Vorname = "aaa" };
            var person2 = new Person() { Name = "bb" };
            List<Person> Persons = new List<Person>()
            {
                person1,
                person2
            };
            
            var pers = PersonHelper.GetPersonsContainingStringInName("a", Persons);
            Assert.IsTrue(pers.Count == 1);
            Assert.IsFalse(pers.Any(p => p.Name == "bbb"));
            CollectionAssert.Contains(pers, person1);
            
        }

    }

}