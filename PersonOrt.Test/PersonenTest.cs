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

        [Test]
        public void CountLettersImplementaions()
        {
            List<Person> Persons = new List<Person>()
            {
                new Person() { Name = "aaa", Vorname = "aaa" },
                new Person() { Name = "bb", Vorname = "bb"}
            };

            //var letters = PersonHelper.CountLettersOfName(Persons);
            var letters = getlastnameLettersKim(Persons);
            Assert.IsTrue(letters['a'] == 3);
            Assert.IsTrue(letters['b'] == 2);
        }

        public string GetBuchstabenAnzahl_AdrianJerbic(List<Person> Persons)
        {
            var a = "a";
            var b = "b";
            var c = "c";
            var d = "d";
            var e = "e";
            var f = "f";
            var g = "g";
            var h = "h";
            var i = "i";
            var j = "j";
            var k = "k";
            var l = "l";
            var m = "m";
            var n = "n";
            var o = "o";
            var p = "p";
            var q = "q";
            var r = "r";
            var s = "s";
            var t = "t";
            var u = "u";
            var v = "v";
            var w = "w";
            var x = "x";
            var y = "y";
            var z = "z";
            int counta = 0;
            int countb = 0;
            int countc = 0;
            int countd = 0;
            int counte = 0;
            int countf = 0;
            int countg = 0;
            int counth = 0;
            int counti = 0;
            int countj = 0;
            int countk = 0;
            int countl = 0;
            int countm = 0;
            int countn = 0;
            int counto = 0;
            int countp = 0;
            int countq = 0;
            int countr = 0;
            int counts = 0;
            int countt = 0;
            int countu = 0;
            int countv = 0;
            int countw = 0;
            int countx = 0;
            int county = 0;
            int countz = 0;
            foreach (var Person in Persons)
            {
                if (Person.Vorname.Contains(a))
                {
                    counta++;
                }



                if (Person.Vorname.Contains(b))
                {
                    countb++;
                }



                if (Person.Vorname.Contains(c))
                {
                    countc++;
                }



                if (Person.Vorname.Contains(d))
                {
                    countd++;
                }



                if (Person.Vorname.Contains(e))
                {
                    counte++;
                }



                if (Person.Vorname.Contains(f))
                {
                    countf++;
                }



                if (Person.Vorname.Contains(g))
                {
                    countg++;
                }



                if (Person.Vorname.Contains(h))
                {
                    counth++;
                }



                if (Person.Vorname.Contains(i))
                {
                    counti++;
                }



                if (Person.Vorname.Contains(j))
                {
                    countj++;
                }



                if (Person.Vorname.Contains(k))
                {
                    countk++;
                }



                if (Person.Vorname.Contains(l))
                {
                    countl++;
                }



                if (Person.Vorname.Contains(m))
                {
                    countm++;
                }



                if (Person.Vorname.Contains(n))
                {
                    countn++;
                }



                if (Person.Vorname.Contains(o))
                {
                    counto++;
                }



                if (Person.Vorname.Contains(p))
                {
                    countp++;
                }



                if (Person.Vorname.Contains(q))
                {
                    countq++;
                }



                if (Person.Vorname.Contains(r))
                {
                    countr++;
                }



                if (Person.Vorname.Contains(s))
                {
                    counts++;
                }



                if (Person.Vorname.Contains(t))
                {
                    countt++;
                }



                if (Person.Vorname.Contains(u))
                {
                    countu++;
                }



                if (Person.Vorname.Contains(v))
                {
                    countv++;
                }



                if (Person.Vorname.Contains(w))
                {
                    countw++;
                }



                if (Person.Vorname.Contains(x))
                {
                    countx++;
                }



                if (Person.Vorname.Contains(y))
                {
                    county++;
                }



                if (Person.Vorname.Contains(z))
                {
                    countz++;
                }
            }
            var count = "a: " + counta + ", b: " + countb + ", c: " + countc + ", d: " + countd + ", e: " + counte + ", f: " + countf
            + ", g: " + countg + ", h: " + counth + ", i: " + counti + ", j: " + countj + ", k: " + countk + ", l: " + countl
            + ", m: " + countm + ", n: " + countn + ", o: " + counto + ", p: " + countp + ", q: " + countq + ", r: " + countr
            + ", s: " + counts + ", t: " + countt + ", u: " + countu + ", v: " + countv + ", w: " + countw + ", x: " + countx
            + ", y: " + county + ", z: " + countz;
            return count;
        }

        //public Dictionary<char, int> AnzahlChars(List<Person> persons)
        //{
        //    Dictionary<char, int> ausgabe = new Dictionary<char, int>();
        //    String allFirstnames;
        //    char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        //    for (var i = 0; i < persons.Count; i++)
        //    {
        //        allFirstnames += persons[i].Vorname;
        //    }
        //    char[] chars = allFirstnames.ToCharArray();
        //    for (int i = 0; i < chars.Count; i++)
        //    {
        //        if (chars[i] == alphabet[i])
        //        {
        //            ausgabe.Add(alphabet[i], 1);

        //        }
        //    }
        //    return ausgabe;
        //}
        public static List<String> getLastNameLettersTestEmanuelHiebeler(List<Person> personList)
        {
            List<Char> chars = new List<Char>();
            List<String> result = new List<String>();
            List<int> charCounter = new List<int>();
            foreach (Person person in personList)
            {
                String lastname = person.Name;
                foreach (char c in lastname)
                {
                    if (!chars.Contains(c))
                    {
                        charCounter.Add(1);
                        chars.Add(c);
                        result.Add(c.ToString());
                    }
                    else
                    {
                        for (int i = 0; i < charCounter.Count; i++)
                        {
                            if (chars[i] == c)
                            {
                                charCounter[i]++;
                                result[i] = c + " " + charCounter[i].ToString();
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static Dictionary<Char, int> getlastnameLettersKim(List<Person> Persons)
        {
            Dictionary<Char, int> lastnameLetters = new Dictionary<Char, int>();
            foreach (Person person in Persons)
            {
                foreach (char c in person.Name.ToLower())
                {
                    if (lastnameLetters.ContainsKey(c))
                    {
                        lastnameLetters[c]++;
                    }
                    else
                    {
                        lastnameLetters[c] = 1;
                    }
                }
            }
            return lastnameLetters;
        }

        public static bool isContains(String nachname, char letter)
        {
            bool value = nachname.Contains(letter);
            return value;
        }

        public string GetLetterAaronHaltmair(List<Person> Persons)
        {
            int a1 = 0;
            int b1 = 0;
            int c1 = 0;

            int Count = 0;
            foreach (var pers in Persons)
            {
                foreach (char c in pers.Name)
                {
                    if (c == 'a')
                    {
                        a1++;
                    }
                    if (c == 'b')
                    {
                        b1++;
                    }
                    if (c == 'c')
                    {
                        c1++;
                    }

                }
            }
            return "a:" + a1 + " b:" + b1 + " c:" + c1;
        }
        public string buchstabenLukasFenkart(List<Person> Persons)
        {
            //int counta = 0;
            //int countb = 0;
            //int countc = 0;
            //int countd = 0;
            //int counte = 0;
            //int countf = 0;
            //int countg = 0;
            //int counth = 0;
            //int counti = 0;
            //int countj = 0;
            //int countk = 0;
            //int countl = 0;
            //int countm = 0;

            //char a = 'a';
            //char b = 'b';
            //char c = 'c';
            //char d = 'd';



            //foreach (var person in Persons)
            //{
            //    foreach (char name in Name)
            //    {
            //        if (Name.Contains(a))
            //        {
            //            counta =+1;
            //        }
            //        else if (Name.Contains(b))
            //        {
            //            countb = +1;
            //        }
            //        else if (Name.Contains(c))
            //        {
            //            countc = +1;
            //        }
            //        else if (Name.Contains(d))
            //        {
            //            countd = +1;
            //        }

            //    }

            //}
            return "a: "; // + counta + " b: " + countb + " c: " + countc + " d: " + countd;

        }
        public Dictionary<char, int> letters(int id, List<Person> Persons)
        {
            var retval = new Dictionary<char, int>();

            foreach (var p in Persons)
            {
                foreach (char c in p.Name)
                {
                    if (!retval.ContainsKey(c))
                        retval.Add(c, 1);
                    else
                        retval[c]++;
                }
            }
            return retval;
        }
    }
}

