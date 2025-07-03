using backend.Context;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class SlotService : ISlotService
    {
        private readonly ApplicationDbContext _context;
        public SlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<Slot>>> GetAllSlotsAsync()
        {
            var slots = await _context.Slots.ToListAsync();
            return new ApiResponse<List<Slot>> { IsSucceeded = true, Data = slots, StatusCode = 200 };
        }

        public async Task<ApiResponse<Slot>> GetSlotByIdAsync(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Slot not found", StatusCode = 404 };
            return new ApiResponse<Slot> { IsSucceeded = true, Data = slot, StatusCode = 200 };
        }

        public async Task<ApiResponse<List<Slot>>> GetSlotsByDayAsync(DateTime date)
        {
            var slots = await _context.Slots.Where(s => s.Date.Date == date.Date).ToListAsync();
            return new ApiResponse<List<Slot>> { IsSucceeded = true, Data = slots, StatusCode = 200 };
        }
         
        public async Task<ApiResponse<List<Slot>>> GetSlotsByWeekAsync(DateTime dateInWeek)
        {
            var startOfWeek = dateInWeek.Date.AddDays(-(int)dateInWeek.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var slots = await _context.Slots.Where(s => s.Date >= startOfWeek && s.Date < endOfWeek).ToListAsync();
            return new ApiResponse<List<Slot>> { IsSucceeded = true, Data = slots, StatusCode = 200 };
        }

        public async Task<ApiResponse<List<Slot>>> GetSlotsByMonthAsync(int year, int month)
        {
            var slots = await _context.Slots.Where(s => s.Date.Year == year && s.Date.Month == month).ToListAsync();
            return new ApiResponse<List<Slot>> { IsSucceeded = true, Data = slots, StatusCode = 200 };
        }

        public async Task<ApiResponse<Slot>> CreateSlotAsync(SlotDto slotDto)
        {
            // 1. Validate time logic
            if (slotDto.StartTime >= slotDto.EndTime)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Start time must be before end time", StatusCode = 400 };
            }

            var duration = (slotDto.EndTime - slotDto.StartTime).TotalMinutes;

            // 2. Check duration
            if (duration > 60)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Session cannot be longer than 60 minutes", StatusCode = 400 };
            }

            // 3. Check duplicate slot
            var existingSlot = await _context.Slots.FirstOrDefaultAsync(s =>
                s.Date == slotDto.Date &&
                s.StartTime == slotDto.StartTime &&
                s.EndTime == slotDto.EndTime);

            if (existingSlot != null)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "This slot already exists", StatusCode = 400 };
            }

            var slot = new Slot
            {
                Date = slotDto.Date,
                StartTime = slotDto.StartTime,
                EndTime = slotDto.EndTime,
                Duration = Math.Round(duration)
            };

            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();

            return new ApiResponse<Slot> { IsSucceeded = true, Data = slot, StatusCode = 201 };
        }

        public async Task<ApiResponse<Slot>> EditSlotAsync(int id, SlotDto slotDto)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Slot not found", StatusCode = 404 };

            if (slotDto.StartTime >= slotDto.EndTime)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Start time must be before end time", StatusCode = 400 };
            }

            var duration = (slotDto.EndTime - slotDto.StartTime).TotalMinutes;

            if (duration > 60)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Session cannot be longer than 60 minutes", StatusCode = 400 };
            }

            // Exclude current slot when checking for duplicates
            var existingSlot = await _context.Slots.FirstOrDefaultAsync(s =>
                s.Id != id &&
                s.Date == slotDto.Date &&
                s.StartTime == slotDto.StartTime &&
                s.EndTime == slotDto.EndTime);

            if (existingSlot != null)
            {
                return new ApiResponse<Slot> { IsSucceeded = false, Error = "Another slot with the same time already exists", StatusCode = 400 };
            }

            slot.Date = slotDto.Date;
            slot.StartTime = slotDto.StartTime;
            slot.EndTime = slotDto.EndTime;
            slot.Duration = Math.Round(duration);

            await _context.SaveChangesAsync();

            return new ApiResponse<Slot> { IsSucceeded = true, Data = slot, StatusCode = 200 };
        }

        public async Task<ApiResponse<string>> DeleteSlotAsync(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
                return new ApiResponse<string> { IsSucceeded = false, Error = "Slot not found", StatusCode = 404 };
            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();
            return new ApiResponse<string> { IsSucceeded = true, Data = "Slot deleted", StatusCode = 200 };
        }
    }
} 