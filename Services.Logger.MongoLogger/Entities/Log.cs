using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Entities
{
    public class Log
    {
        [BsonElement("log_id")]
        public string LogId { get; set; }

        public string Message { get; set; }
    }
}
