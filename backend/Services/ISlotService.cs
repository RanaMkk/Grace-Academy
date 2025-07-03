using backend.DTOs;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface ISlotService
    {
        Task<ApiResponse<List<Slot>>> GetAllSlotsAsync();
        Task<ApiResponse<Slot>> GetSlotByIdAsync(int id);
        Task<ApiResponse<List<Slot>>> GetSlotsByDayAsync(DateTime date);
        Task<ApiResponse<List<Slot>>> GetSlotsByWeekAsync(DateTime dateInWeek);
        Task<ApiResponse<List<Slot>>> GetSlotsByMonthAsync(int year, int month);
        Task<ApiResponse<Slot>> CreateSlotAsync(SlotDto slotDto);
        Task<ApiResponse<string>> DeleteSlotAsync(int id);
        Task<ApiResponse<Slot>> EditSlotAsync(int id, SlotDto slotDto);
    }
} 