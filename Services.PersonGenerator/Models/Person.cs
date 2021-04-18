using Services.PersonGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.PersonGenerator.Models
{
    public class Person : IMockData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => ((DateTime.Now - BirthDate.Date).Days / 360);
    }
}
