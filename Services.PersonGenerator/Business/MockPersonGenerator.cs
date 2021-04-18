using Services.PersonGenerator.Interfaces;
using Services.PersonGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.PersonGenerator.Business
{
    public class MockPersonGenerator : IMockDataGenerator
    {
        Random _random;
        int MAX_AGE = 80;

        public MockPersonGenerator()
        {
            _random = new Random();
        }

        public IMockData GetMockData()
        {
            var age = _random.Next(MAX_AGE);

            var person = new Person()
            {
                BirthDate = DateTime.Now.AddYears(-age),
                Name = "Tuğkan",
                Surname = "Meral"
            };
            return person;
        }

        public IEnumerable<IMockData> GetMockDatas(int count)
        {
            var people = new List<IMockData>();

            for (int i = 0; i < count; i++)
            {
                people.Add(GetMockData());
            }

            return people;
        }
    }
}
