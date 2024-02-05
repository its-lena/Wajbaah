using Wajbah_API.Models;

namespace Wajbah_API.Services
{
    public interface IMessageService
    {
        Task CreateAsync(Message message);

        Task<List<Message>> GetAllAsync();

        Task<Message> GetById(string id);

        Task UpdateAsync(string id, Message message);

        Task DeleteAsync(string id);
    }
}