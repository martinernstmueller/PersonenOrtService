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


        [Test]
        public void GetPersons_WithPLZ_ReturnsPersonsWithPLZ()
        {
            var dornbirn = new Ort()
            {
                Name = "Dornbirn",
                PLZ = "6850"
            };
            var luschtnou = new Ort()
            {
                Name = "Luschtnou",
                PLZ = "6890"
            };

            var person1 = new Person() { Name = "aaa", Vorname = "aaa", 
                Ort = dornbirn };
            var person2 = new Person() { Name = "bb" ,
                Ort = luschtnou
            };
       
            List<Person> Persons = new List<Person>()
            {
                person1,
                person2
            };

            var pers = PersonHelper.GetPersonsWithPLZ("6850", Persons);
            Assert.IsTrue(pers.Count == 1);
            Assert.IsTrue(pers.Any(p => p.Ort.PLZ == "6850"));
            var cont = pers.Contains(person1);
        }

        public List<Person> GetStringfromPerson_AdrianJerbic(string wantedString, List<Person> Persons)
        {
            var retval = new List<Person>();
            foreach (Person person in Persons)
            {
                if (person.Vorname.Contains(wantedString))
                {
                    retval.Add(person);
                }
            }
            return retval;
        }

        public static IEnumerable<String> PersonEnthaeltBuchstaben(List<Person> persons, string enthalten)
        {
            List<String> ergebnis = new List<String>();

            foreach (Person person in persons)
            {
                String name = person.Name;
                if (name.Contains(enthalten))
                {
                    ergebnis.Add(person.ToString());
                }
            }
            return ergebnis;
        }

        public List<String> PersonsContaining(String value, List<Person> Persons)
        {
            var list = new List<String>();
            
            foreach (var person in Persons)
            {
                String nachname = person.Name;
                
                if (nachname.Contains(value) )
                {
                    list.Add(nachname);
                }
            }
            return list;
        }


        //[Test]
        //public void testMath()
        //{
        //    double a = 10.0;
        //    double b = a/7;
        //    var c = Math.Sqrt(Math.Sqrt(Math.Sqrt(Math.Sqrt(b))));

        //    var d = Math.Exp(16*Math.Log(c));
        //    var e = d*7;
        //    var f = a-e;
        //    Console.Write(f);
        //}

    }

}