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

        //public Person Person { get; set; }
        //public Log()
        //{
        //    Person = new Person("Tuğkan", "Meral");
        //}
    }

    //public class Person
    //{
    //    public string Name { get; set; }
    //    public string Surname { get; set; }
    //    public List<Parent> Parents { get; set; }

    //    public Person(string name, string surname)
    //    {
    //        Name = name;
    //        Surname = surname;

    //        Parents = new List<Parent>();
    //        Parents.Add(new Parent() { Name = "Emine" });
    //        //Parents.Add(new Parent() { Name = "Engin" });
    //    }
    //}
    //public class Parent
    //{
    //    public string Name { get; set; }
    //}
}
