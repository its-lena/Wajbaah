using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wajbah_API.Models
{
    public class Conversation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Cutomer_Id { get; set; }
        public string Chef_Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
