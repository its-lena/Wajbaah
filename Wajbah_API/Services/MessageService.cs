using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Wajbah_API.Models;

namespace Wajbah_API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMongoCollection<Message> _MessageCollection;

        public MessageService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _MessageCollection = database.GetCollection<Message>(mongoDBSettings.Value.MessageCollectionName);
        }


        public async Task CreateAsync(Message message)
        {
            await _MessageCollection.InsertOneAsync(message);
            return;
        }

        //public async Task<IEnumerable<Message>> GetAllAsync()
        //{
        //    var pipeline = new BsonDocument[]
        //    {
        //        new BsonDocument("$lookup", new BsonDocument
        //        {
        //            { "from", "ConversationCollection" },
        //            { "localField", "ConversationId" },
        //            { "foreignField", "Id" },
        //            { "as", "Message_Convesation" }
        //        }),
        //        new BsonDocument("$unwind", "Message_Convesation"),
        //        new BsonDocument("$project", new BsonDocument
        //        {
        //            {"Id", 1 },
        //            {"ConversationId", 1 },
        //            {"MessageContent", 1 },
        //            {"ConversationName", "$Message_Convesation.ConversationName" }
        //        })
        //    };

        //    var result = await _MessageCollection.Aggregate<Message>(pipeline).ToListAsync();
        //    return result;
        //}


        public async Task<List<Message>> GetAllAsync()
        {
            return await _MessageCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Message> GetById(string id) =>
            await _MessageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Message message) =>
            await _MessageCollection.ReplaceOneAsync(x => x.Id == id, message);


        public async Task DeleteAsync(string id)
        {
            await _MessageCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
