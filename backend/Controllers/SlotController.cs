using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSlots()
        {
            var response = await _slotService.GetAllSlotsAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSlotById(int id)
        {
            var response = await _slotService.GetSlotByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("day/{date}")]
        public async Task<IActionResult> GetSlotsByDay(DateTime date)
        {
            var response = await _slotService.GetSlotsByDayAsync(date);
            return Ok(response);
        }

        [HttpGet("week/{dateInWeek}")]
        public async Task<IActionResult> GetSlotsByWeek(DateTime dateInWeek)
        {
            var response = await _slotService.GetSlotsByWeekAsync(dateInWeek);
            return Ok(response);
        }

        [HttpGet("month/{year}/{month}")]
        public async Task<IActionResult> GetSlotsByMonth(int year, int month)
        {
            var response = await _slotService.GetSlotsByMonthAsync(year, month);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlot([FromBody] SlotDto slotDto)
        {
            var response = await _slotService.CreateSlotAsync(slotDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var response = await _slotService.DeleteSlotAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSlot(int id, [FromBody] SlotDto slotDto)
        {
            var response = await _slotService.EditSlotAsync(id, slotDto);
            return Ok(response);
        }
    }
} 