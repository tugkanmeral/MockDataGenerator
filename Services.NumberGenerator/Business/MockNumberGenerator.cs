using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.ServiceBase.MockDataGenerator;

namespace Services.NumberGenerator.Business
{
    public class MockNumberGenerator : IMockDataGenerator
    {
        public IMockData GetMockData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMockData> GetMockDatas(int count)
        {
            throw new NotImplementedException();
        }
    }
}
