using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonenOrt.Framework
{
    public class Ort
    {
        public Ort(string name, string pLZ)
        {
            Name = name;
            PLZ = pLZ;
        }

        [Key]
        public string PLZ { get; set; }
        public string Name { get; set; }
    }
}
