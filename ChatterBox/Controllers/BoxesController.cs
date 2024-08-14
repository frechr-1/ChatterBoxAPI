using Microsoft.AspNetCore.Mvc;
using ChatterBox.Entities;
using ChatterBox.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatterBox.DTO.Responses.Box;
using ChatterBox.DTO.Requests.Box;

namespace ChatterBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;

        public BoxesController(IBoxRepository boxRepository, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _mapper = mapper;
        }

        // GET: api/Boxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBoxDto>>> GetBoxes([FromQuery] string? boxName)
        {
            IEnumerable<Box> boxes;

            if (!string.IsNullOrEmpty(boxName))
            {
                boxes = await _boxRepository.GetByNameAsync(boxName);
            }
            else
            {
                boxes = await _boxRepository.GetAllAsync();
            }

            if (boxes == null || !boxes.Any())
            {
                return NotFound("No boxes found.");
            }

            var boxDtos = _mapper.Map<IEnumerable<GetBoxDto>>(boxes);
            return Ok(boxDtos);
        }

        // GET: api/Boxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBoxDto>> GetBox(int id)
        {
            var box = await _boxRepository.GetByIdAsync(id);
            if (box == null)
            {
                return NotFound($"Box with id {id} not found.");
            }
            var boxDto = _mapper.Map<GetBoxDto>(box);
            return Ok(boxDto);
        }

        // PUT: api/Boxes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBox(int id, [FromBody] PutBoxDto boxDto)
        {
            if (id != boxDto.Id)
            {
                return BadRequest("The provided ID does not match the Box ID.");
            }

            if (!await _boxRepository.ExistsAsync(id))
            {
                return NotFound($"Box with id {id} not found.");
            }
            var box = await _boxRepository.GetByIdAsync(id);
            box.BoxName = boxDto.BoxName;
            box.Agenda = boxDto.Agenda;
            var result = await _boxRepository.UpdateAsync(box);

            if (result > 0)
            {
                return Ok(_mapper.Map<GetBoxDto>(box));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the box.");
            }
            
        }

        // POST: api/Boxes
        [HttpPost]
        public async Task<ActionResult<GetBoxDto>> PostBox(PostBoxDto boxDto)
        {
            var box = _mapper.Map<Box>(boxDto);
            var result = await _boxRepository.AddAsync(box);
            if( result > 0)
            {
                return CreatedAtAction(nameof(GetBox), new { id = box.Id }, box);

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new box.");
            }
        }

        // DELETE: api/Boxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBox(int id)
        {
            if (!await _boxRepository.ExistsAsync(id))
            {
                return NotFound($"Box with id {id} not found.");
            }

            var result = await _boxRepository.DeleteAsync(id);
            if( result > 0)
            {
                return NoContent();

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting box.");
            }
        }
    }
}
