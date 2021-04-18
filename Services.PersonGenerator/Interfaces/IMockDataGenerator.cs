using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.PersonGenerator.Interfaces
{
    public interface IMockDataGenerator
    {
        IMockData GetMockData();
        IEnumerable<IMockData> GetMockDatas(int count);
    }
}
