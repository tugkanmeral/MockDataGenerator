using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.ServiceBase.MockDataGenerator
{
    public interface IMockDataGenerator
    {
        IMockData GetMockData();
        IEnumerable<IMockData> GetMockDatas(int count);
    }
}
