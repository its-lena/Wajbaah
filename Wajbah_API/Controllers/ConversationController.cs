using Microsoft.AspNetCore.Mvc;
using Wajbah_API.Models;
using Wajbah_API.Services;

namespace Wajbah_API.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        public async Task<List<Conversation>> Get()
        {
            return await _conversationService.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Conversation conversation)
        {
            await _conversationService.CreateAsync(conversation);
            return CreatedAtAction(nameof(Get), new { id = conversation.Id }, conversation);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var converstion = await _conversationService.GetById(id);
            if (converstion == null)
            {
                return NotFound();
            }
            return Ok(converstion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Conversation newConversation)
        {
            var conversation = await _conversationService.GetById(id);
            if (conversation == null)
                return NotFound();
            await _conversationService.UpdateAsync(id, newConversation);
            return Ok("Updated Succefully");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var conversation = await _conversationService.GetById(id);
            if (conversation == null)
                return NotFound();
            await _conversationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
