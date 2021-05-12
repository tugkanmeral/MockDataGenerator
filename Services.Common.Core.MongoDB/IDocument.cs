using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Core.MongoDB
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}
