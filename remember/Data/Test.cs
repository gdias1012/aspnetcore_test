using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace remember.Data
{
    public class Test
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name { get; set; }
    }
}
