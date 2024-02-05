using Wajbah_API.Models;

namespace Wajbah_API.Services
{
    public interface IConversationService
    {
        Task CreateAsync(Conversation conversation);

        Task<List<Conversation>> GetAllAsync();

        Task<Conversation> GetById(string id);

        Task UpdateAsync(string id, Conversation conversation);

        Task DeleteAsync(string id);
    }
}