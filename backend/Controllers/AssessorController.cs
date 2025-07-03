using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessorController : ControllerBase
    {
        private readonly IAssessorService _assessorService;
        public AssessorController(IAssessorService assessorService)
        {
            _assessorService = assessorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AssessorResponseDto>>>> GetAllAssessors()
        {
            var result = await _assessorService.GetAllAssessorsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AssessorResponseDto>>> GetAssessorById(int id)
        {
            var result = await _assessorService.GetAssessorByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<ApiResponse<AssessorResponseDto>>> GetAssessorByUsername(string username)
        {
            var result = await _assessorService.GetAssessorByUsernameAsync(username);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> EditAssessor(int id, [FromBody] EditAssessorDto dto)
        {
            var result = await _assessorService.EditAssessorAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}/pick-slot/{slotId}")]
        public async Task<ActionResult<ApiResponse<bool>>> PickSlot(int id, int slotId)
        {
            var result = await _assessorService.PickSlotAsync(id, slotId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}/remove-slot/{slotId}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveSlot(int id, int slotId)
        {
            var result = await _assessorService.RemoveSlotAsync(id, slotId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAssessor(int id)
        {
            var result = await _assessorService.DeleteAssessorAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/available-slots")]
        public async Task<ActionResult<ApiResponse<IEnumerable<SlotDto>>>> GetAvailableSlotsByAssessorId(int id)
        {
            var result = await _assessorService.GetAvailableSlotsByAssessorIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
} 
