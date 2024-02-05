using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Wajbah_API.Models;

namespace Wajbah_API.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IMongoCollection<Conversation> _ConversationCollection;

        public ConversationService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ConversationCollection = database.GetCollection<Conversation>(mongoDBSettings.Value.ConversationCollectionName);
        }


        public async Task CreateAsync(Conversation conversation)
        {
            await _ConversationCollection.InsertOneAsync(conversation);
            return;
        }

        public async Task<List<Conversation>> GetAllAsync()
        {
            return await _ConversationCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Conversation> GetById(string id) =>
            await _ConversationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Conversation conversation) =>
            await _ConversationCollection.ReplaceOneAsync(x => x.Id == id, conversation);


        public async Task DeleteAsync(string id)
        {
            await _ConversationCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
