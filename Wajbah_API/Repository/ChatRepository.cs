using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
    public class ChatRepository : IChatRepository
    { 
        private readonly IMongoCollection<Message> _MessageCollection;
        private readonly IMongoCollection<Conversation> _ConversationCollection;

        public ChatRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _MessageCollection = database.GetCollection<Message>(mongoDBSettings.Value.MessageCollectionName);
            _ConversationCollection = database.GetCollection<Conversation>(mongoDBSettings.Value.ConversationCollectionName);
        }

        public async Task CreateAsync(Conversation conversation)
        {
            await _ConversationCollection.InsertOneAsync(conversation);
            return;
        }

        public async Task<Conversation> GetConversationById(Guid id) =>
            await _ConversationCollection.Find(x => x.ConversationId == id).FirstOrDefaultAsync();

        public async Task<List<Message>> GetMessagesByConversationId(Guid Id)
        {
            return await _MessageCollection.Find(x => x.ConversationId == Id).ToListAsync();
        }


        public async Task SendMessage(Guid conversationId, Message message)
        {
            if (conversationId != null)
            {
                await _MessageCollection.InsertOneAsync(message);
                return;
            }
        }

        public async Task<List<Conversation>> GetAllConversationsAsync()
        {
            return await _ConversationCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task DeleteConversationAsync(Guid id)
        {
            await _ConversationCollection.DeleteManyAsync(x => x.ConversationId == id);
        }

        public async Task DeleteMessagesAsync(Guid id)
        {
            await _MessageCollection.DeleteOneAsync(x => x.ConversationId == id);
        }
    }
}
