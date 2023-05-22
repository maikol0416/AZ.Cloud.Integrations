using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Common
{

    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreation = DateTime.UtcNow;
            DateLastUpdate = null;
            Status = States.Active.ToString();

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Status { get; set; }
        public DateTime? DateLastUpdate { get; set; }
    }
}
