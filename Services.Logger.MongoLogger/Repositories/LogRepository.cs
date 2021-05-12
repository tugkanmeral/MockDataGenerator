using MongoDB.Driver;
using Services.Common.Core.MongoDB;
using Services.Logger.MongoLogger.Entities;

namespace Services.Logger.MongoLogger.Repositories
{
    public class LogRepository : MongoRepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IMongoClient client) : base(client)
        {

        }
    }
}
