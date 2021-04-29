using Services.Common.ServiceBase.MockDataGenerator;
using Services.PersonGenerator.Datas.Seeds;
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
            var nameIndex = _random.Next(1, 5);
            var surnameIndex = _random.Next(1, 5);

            var person = new Person()
            {
                BirthDate = DateTime.Now.AddYears(-age),
                Name = NameSeeds.Names[nameIndex],
                Surname = SurnameSeeds.Surnames[surnameIndex],
                Age = age
            };
            return person;
        }

        public IEnumerable<IMockData> GetMockDatas(int count)
        {
            var people = new List<Person>();

            for (int i = 0; i < count; i++)
            {
                people.Add((Person)GetMockData());
            }

            return people;
        }
    }
}
