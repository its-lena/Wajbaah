using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public interface IChatRepository
    {
        Task CreateAsync(Conversation conversation);

        Task SendMessage(Guid conversationId, Message message);

        Task<List<Conversation>> GetAllConversationsAsync();

        Task<Conversation> GetConversationById(Guid id);

        Task<List<Message>> GetMessagesByConversationId(Guid id);

        Task DeleteConversationAsync(Guid id);

        Task DeleteMessagesAsync(Guid id);
    }
}
