using Services.Common.Core.MongoDB;
using Services.Logger.MongoLogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {

    }
}
