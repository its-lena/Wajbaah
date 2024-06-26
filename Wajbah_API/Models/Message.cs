﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wajbah_API.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string senderId { get; set; }
        public Guid ConversationId { get; set; } 
        [BsonIgnoreIfNull]
        public string MessageContent { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
