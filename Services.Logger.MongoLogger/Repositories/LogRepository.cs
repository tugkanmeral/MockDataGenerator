using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Services.Logger.MongoLogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Repositories
{
    public class LogRepository
    {
        private readonly IMongoCollection<Log> _logCollection;

        public LogRepository(IMongoClient mongoClient)
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

            _logCollection = mongoClient.GetDatabase("mock_data_generator").GetCollection<Log>("logs");
        }

        public async void Insert(string message)
        {
            Log newLog = new Log()
            {
                Message = message
            };
            await _logCollection.InsertOneAsync(newLog);
        }
    }
}
