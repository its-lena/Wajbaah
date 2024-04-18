using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wajbah_API.Models.DTO.Chats
{
    public class ConversationDTO
    {
        [BsonId]
        public Guid ConversationId { get; set; }
    }
}
