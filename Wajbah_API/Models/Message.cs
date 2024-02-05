using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wajbah_API.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; }

        [BsonIgnoreIfNull]
        public string ConversationName { get; set; }
        public string CustomerId { get; set; }
        public string ChefId { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
