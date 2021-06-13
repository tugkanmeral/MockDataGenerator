using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Services.Common.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Entities
{
    public partial class Log : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Message { get; set; }
    }
}
