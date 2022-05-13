namespace PersonenOrt.Framework
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Alter { get => GetAge(); }
        public string? Vorname { get; set; }

        public Ort? Ort { get; set; }

        public DateTime? Geburtsdatum { get; set; }

        public int GetAge()
        {
            if (Geburtsdatum == null)
                return -1;
            DateTime gebNotNull = (DateTime)Geburtsdatum;
            var age = DateTime.Now.Year -  gebNotNull.Year;
            if (DateTime.Now.DayOfYear <= gebNotNull.DayOfYear)
                age--;
            return age;
        }

    }
}