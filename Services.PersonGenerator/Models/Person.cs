using Services.Common.ServiceBase.MockDataGenerator;
using System;

namespace Services.PersonGenerator.Models
{
    public class Person : IMockData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
