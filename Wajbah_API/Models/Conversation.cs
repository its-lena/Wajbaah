using MongoDB.Bson.Serialization.Attributes;

namespace Wajbah_API.Models
{
    public class Conversation
    {
        [BsonId]
        public Guid ConversationId { get; set; }

        public string Title { get; set; }
        //public string Sender{ get; set; }
        //public string Recipient { get; set; }
        public List<string> Participants { get; set; }
        [BsonRequired]
        public List<Message>? Messages { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
