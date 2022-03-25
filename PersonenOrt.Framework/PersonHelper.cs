using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonenOrt.Framework
{
    public class PersonHelper
    {
        public static List<Person> Persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "Hinz",
                Ort = new Ort { PLZ="6850", Name="Dornbirn" },
                Vorname = "aus",
                Geburtsdatum = new DateTime(2001, 6, 5)
            },
            new Person()
            {
                Id = 2,
                Name = "Kunz",
                Ort = new Ort { PLZ="6890", Name="Lustenau" },
                Vorname = "aus",
                Geburtsdatum= new DateTime(2000, 1, 4)
            }
        };
        public static List<Ort> Orts = new List<Ort>();

        public static double GetMeanAge(List<Person> persons)
        {
            double summeAlter = 0;
            foreach (var person in persons)
            {
                summeAlter += person.GetAge();
            }
            double mean = summeAlter / persons.Count;
            return mean;
        }
    }
}
