using Microsoft.AspNetCore.Mvc; 
using ChatterBox.Interfaces;
using ChatterBox.Entities;
using AutoMapper;
using ChatterBox.DTO.Responses.ChatBox;
using ChatterBox.DTO.Requests.ChatBox;

namespace ChatterBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBoxesController : ControllerBase
    {
        private readonly IChatBoxRepository _chatBoxRepository;
        private readonly IMapper _mapper;
        public ChatBoxesController(IChatBoxRepository chatBoxRepository, IMapper mapper)
        {
            _chatBoxRepository = chatBoxRepository;
            _mapper = mapper;
        }

        // GET: api/ChatBoxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetChatBoxDto>>> GetChatBoxes([FromQuery] int? boxId)
        {
            IEnumerable<ChatBox> result;

            if (boxId.HasValue)
            {
                result = await _chatBoxRepository.GetByBoxIdAsync(boxId.Value);
            }
            else
            {
                result = await _chatBoxRepository.GetAllAsync();
            }

            if (!result.Any())
            {
                return NotFound($"No chatboxes with boxid {boxId} found.");
            }

            return Ok(_mapper.Map<IEnumerable<GetChatBoxDto>>(result));
        }

        // GET: api/ChatBoxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetChatBoxDto>> GetChatBox(int id)
        {
            var chatBox = await _chatBoxRepository.GetByIdAsync(id);

            if (chatBox == null)
            {
                return NotFound($"Chatbox with id {id} not found.");
            }

            return Ok(_mapper.Map<GetChatBoxDto> (chatBox));
        }

        // PUT: api/ChatBoxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatBox(int id, PutChatBoxDto chatBox)
        {
            if (id != chatBox.Id)
            {
                return BadRequest("The provided ID does not match the ChatBox ID.");
            }

            if (!await _chatBoxRepository.ExistsAsync(id))
            {
                return NotFound($"No chatbox with ID {id} found.");
            }

            var updateChatBox = await _chatBoxRepository.GetByIdAsync(chatBox.Id);
            updateChatBox.Date = DateTime.UtcNow;
            updateChatBox.Content = chatBox.Content;
            updateChatBox.ImageUrl = chatBox.ImageUrl;
            updateChatBox.Name = chatBox.Name;
            var result = await _chatBoxRepository.UpdateAsync(updateChatBox);

            if (result > 0)
            {
                return Ok(_mapper.Map<GetChatBoxDto>(updateChatBox));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating chat box with ID {chatBox.Id}.");
            }
        }

        // POST: api/ChatBoxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatBox>> PostChatBox(PostChatBoxDto chatBox)
        {
            var newChatBox = _mapper.Map<ChatBox>(chatBox);

            // Set the new date to the current time
            newChatBox.Date = DateTime.UtcNow;

            var result = await _chatBoxRepository.AddAsync(newChatBox);

            if (result > 0)
            {
                var getChatBoxDto = _mapper.Map<GetChatBoxDto>(newChatBox);
                return CreatedAtAction(nameof(GetChatBox), new { id = newChatBox.Id }, getChatBoxDto);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new chat box");
            }
        }

        // DELETE: api/ChatBoxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatBox(int id)
        {
            var result = await _chatBoxRepository.DeleteAsync(id);
            if (result > 0)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Chatbox with ID {id} not found.");
            }
        }
    }
}
