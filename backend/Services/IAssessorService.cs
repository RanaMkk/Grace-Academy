using backend.DTOs;
using backend.Models;

namespace backend.Services
{
    public interface IAssessorService
    {
        Task<ApiResponse<IEnumerable<AssessorResponseDto>>> GetAllAssessorsAsync();
        Task<ApiResponse<AssessorResponseDto>> GetAssessorByIdAsync(int id);
        Task<ApiResponse<AssessorResponseDto>> GetAssessorByUsernameAsync(string username);
        Task<ApiResponse<bool>> EditAssessorAsync(int id, EditAssessorDto dto);
        Task<ApiResponse<bool>> PickSlotAsync(int assessorId, int slotId);
        Task<ApiResponse<bool>> RemoveSlotAsync(int assessorId, int slotId);
        Task<ApiResponse<bool>> DeleteAssessorAsync(int id);
        Task<ApiResponse<IEnumerable<SlotDto>>> GetAvailableSlotsByAssessorIdAsync(int assessorId);
    }
} 