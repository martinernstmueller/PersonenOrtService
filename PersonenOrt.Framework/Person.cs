using System.Runtime.Serialization;

namespace PersonenOrt.Framework
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Alter { get => GetAge(); }
        public string Vorname { get; set; }

        public Ort Ort { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public int GetAge()
        {
            var age = DateTime.Now.Year -  Geburtsdatum.Year;
            if (DateTime.Now.DayOfYear <= Geburtsdatum.DayOfYear)
                age--;
            return age;
        }

    }
}