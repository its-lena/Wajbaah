﻿namespace Wajbah_API.Data
{
    public class MongoDBSettings 
    {
        public string ConnectionURL { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string ConversationCollectionName { get; set; } = null;
        public string MessageCollectionName { get; set; } = null;
    }
}
