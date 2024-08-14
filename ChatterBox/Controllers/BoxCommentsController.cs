using Microsoft.AspNetCore.Mvc;
using ChatterBox.Entities;
using ChatterBox.Interfaces;
using AutoMapper;
using ChatterBox.DTO.Responses.BoxComment;
using ChatterBox.DTO.Requests.BoxComment;

namespace ChatterBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxCommentsController : ControllerBase
    {
        private readonly IBoxCommentRepository _boxCommentRepository;
        private readonly IMapper _mapper;

        public BoxCommentsController(IBoxCommentRepository boxCommentRepository, IMapper mapper)
        {
            _boxCommentRepository = boxCommentRepository;
            _mapper = mapper;
        }

        // GET: api/BoxComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBoxCommentDto>>> GetBoxComments([FromQuery] int? chatBoxId)
        {
            IEnumerable<BoxComment> comments;

            if (chatBoxId.HasValue)
            {
                comments = await _boxCommentRepository.GetByChatBoxIdAsync(chatBoxId.Value);
            }
            else
            {
                comments = await _boxCommentRepository.GetAllAsync();
            }
            if (!comments.Any())
            {
                return NotFound("No box comments found.");
            }
            return Ok(_mapper.Map<IEnumerable<GetBoxCommentDto>>( comments));
        }

        // GET: api/BoxComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBoxCommentDto>> GetBoxComment(int id)
        {
            var boxComment = await _boxCommentRepository.GetByIdAsync(id);
            if (boxComment == null)
            {
                return NotFound($"BoxComment with id {id} not found.");
            }
            return Ok(_mapper.Map< GetBoxCommentDto>( boxComment));
        }

        // PUT: api/BoxComments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoxComment(int id, PutBoxCommentDto boxComment)
        {
            if (id != boxComment.Id)
            {
                return BadRequest("The provided ID does not match the BoxComment ID.");
            }

            if (!await _boxCommentRepository.ExistsAsync(id))
            {
                return NotFound($"BoxComment with id {id} not found.");
            }
            var updateBoxComment = await _boxCommentRepository.GetByIdAsync(boxComment.Id);
            updateBoxComment.Date = DateTime.UtcNow;
            updateBoxComment.Content = boxComment.Content;
            updateBoxComment.ImageUrl = boxComment.ImageUrl;
            var result = await _boxCommentRepository.UpdateAsync(updateBoxComment);

            if (result > 0)
            {
                return Ok(_mapper.Map<GetBoxCommentDto>(updateBoxComment));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the box comment.");
            }
        }

        // POST: api/BoxComments
        [HttpPost]
        public async Task<ActionResult<BoxComment>> PostBoxComment(PostBoxCommentDto boxComment)
        {
            var addBoxComment = _mapper.Map<BoxComment>(boxComment);
            addBoxComment.Date = DateTime.UtcNow;
            var result = await _boxCommentRepository.AddAsync(addBoxComment);
            if( result > 0)
            {
                return CreatedAtAction(nameof(GetBoxComment), new { id = addBoxComment.Id }, _mapper.Map<GetBoxCommentDto>(addBoxComment));

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new box comment.");
            }
        }

        // DELETE: api/BoxComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoxComment(int id)
        {
            if (!await _boxCommentRepository.ExistsAsync(id))
            {
                return NotFound($"BoxComment with id {id} not found.");
            }

            var result = await _boxCommentRepository.DeleteAsync(id);
            if( result > 0)
            {
                return NoContent();

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting box comment.");
            }
        }
    }
}
