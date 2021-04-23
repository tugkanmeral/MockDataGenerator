using Services.PersonGenerator.Models;
using System.Collections.Generic;

namespace Services.PersonGenerator.Interfaces
{
    public interface IMockDataGenerator
    {
        IMockData GetMockData();
        IEnumerable<IMockData> GetMockDatas(int count);
    }
}
