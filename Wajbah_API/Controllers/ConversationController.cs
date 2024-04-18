using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO.Chats;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class ConversationController : Controller
    {
        private readonly IChatRepository _dbItem;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public ConversationController( IChatRepository dbItem, IMapper mapper)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet("Conversation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetConversations()
        {
            try
            {
                IEnumerable<Conversation> conversation = await _dbItem.GetAllConversationsAsync();
                _response.Result = _mapper.Map<List<ConversationDTO>>(conversation);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateConversation([FromBody] ConversationRequest Addconversation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (Addconversation == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var conversation = new Conversation
                {
                    ConversationId = Guid.NewGuid(),
                    Participants = new List<string> { Addconversation.CustomerId, Addconversation.ChefId },
                    Messages = new List<Message>()
                };
                await _dbItem.CreateAsync(conversation);

                return Ok(new ConversationDTO { ConversationId = conversation.ConversationId });
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteConversation(Guid id)
        {
            try
            {
                var Messages = await _dbItem.GetMessagesByConversationId(id);
                foreach (var message in Messages)
                {
                    if (Messages == null)
                        return NotFound();
                    await _dbItem.DeleteMessagesAsync(id);
                }

                var conversation = await _dbItem.GetConversationById(id);
                if (conversation == null)
                    return NotFound();
                await _dbItem.DeleteConversationAsync(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpGet("Messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetMessages(Guid id)
        {
            try
            {
                IEnumerable<Message> Messages = await _dbItem.GetMessagesByConversationId(id);
                _response.Result = _mapper.Map<List<MessageDTO>>(Messages);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPost("Messages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> SendMessage(Guid conversationId, [FromBody] MessageCreateDTO sendMessage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (sendMessage == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Message message = _mapper.Map<Message>(sendMessage);
                message.ConversationId = conversationId;
                await _dbItem.SendMessage(conversationId, message);
                return CreatedAtAction(nameof(GetMessages), new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }
    }
}
