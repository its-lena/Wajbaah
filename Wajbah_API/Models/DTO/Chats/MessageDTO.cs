using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wajbah_API.Models.DTO.Chats
{
    public class MessageDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string senderId { get; set; }
        public string ConversationId { get; set; }
        [BsonIgnoreIfNull]
        public string MessageContent { get; set; }
    }
}
