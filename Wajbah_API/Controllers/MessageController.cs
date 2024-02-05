using Microsoft.AspNetCore.Mvc;
using Wajbah_API.Models;
using Wajbah_API.Services;

namespace Wajbah_API.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _MessageService;

        public MessageController(IMessageService messageService)
        {
            _MessageService = messageService;
        }

        [HttpGet]
        public async Task<List<Message>> Get()
        {
            return await _MessageService.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            message.ConversationName = null;
            await _MessageService.CreateAsync(message);
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var message = await _MessageService.GetById(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Message newMessage)
        {
            newMessage.ConversationName = null;
            var message = await _MessageService.GetById(id);
            if (message == null)
                return NotFound();
            await _MessageService.UpdateAsync(id, newMessage);
            return Ok("Updated Succefully");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var message = await _MessageService.GetById(id);
            if (message == null)
                return NotFound();
            await _MessageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
