using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend.Context;
namespace backend.Services
{
    public class AssessorService : IAssessorService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AssessorService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private AssessorResponseDto MapToDto(Assessor assessor)
        {
            var user = assessor.User;
            return new AssessorResponseDto
            {
                AssessorID = assessor.Id,
                UserID = assessor.UserId,
                Bio = assessor.Bio,
                FName = user?.FirstName,
                LName = user?.LastName,
                Collage = assessor.College,
                Uni = assessor.University,
                YearsOfExp = assessor.YearsOfExperience,
                Specilization = assessor.Specializations,
                Langugues = assessor.LanguagesSpoken,
                IsNative = assessor.IsNative,
                ImgUrl = user?.ImgUrl,
                Nationaloty = user?.Nationality,
                Day = user?.day ?? 0,
                Year = user?.year ?? 0,
                Month = user?.month ?? 0,
                Email = user?.Email,
                Username = user?.UserName
            };
        }

        public async Task<ApiResponse<IEnumerable<AssessorResponseDto>>> GetAllAssessorsAsync()
        {
            var assessors = await _context.Assessors.Include(a => a.User).ToListAsync();
            var dtos = assessors.Select(MapToDto).ToList();
            return new ApiResponse<IEnumerable<AssessorResponseDto>>
            {
                Data = dtos,
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<AssessorResponseDto>> GetAssessorByIdAsync(int id)
        {
            var assessor = await _context.Assessors.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
            if (assessor == null)
            {
                return new ApiResponse<AssessorResponseDto>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            return new ApiResponse<AssessorResponseDto>
            {
                Data = MapToDto(assessor),
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<AssessorResponseDto>> GetAssessorByUsernameAsync(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return new ApiResponse<AssessorResponseDto>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "User not found"
                };
            }
            var assessor = await _context.Assessors.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (assessor == null)
            {
                return new ApiResponse<AssessorResponseDto>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            return new ApiResponse<AssessorResponseDto>
            {
                Data = MapToDto(assessor),
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<bool>> EditAssessorAsync(int id, EditAssessorDto dto)
        {
            var assessor = await _context.Assessors.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
            if (assessor == null)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            // Update assessor fields
            assessor.Bio = dto.Bio;
            assessor.College = dto.Collage;
            assessor.University = dto.Uni;
            assessor.YearsOfExperience = dto.YearsOfExp;
            assessor.Specializations = dto.Specilization;
            assessor.LanguagesSpoken = dto.Langugues;
            assessor.IsNative = dto.IsNative;
            // Update user fields
            if (assessor.User != null)
            {
                assessor.User.FirstName = dto.FName ?? assessor.User.FirstName;
                assessor.User.LastName = dto.LName ?? assessor.User.LastName;
                assessor.User.ImgUrl = dto.ImgUrl ?? assessor.User.ImgUrl;
                assessor.User.Nationality = dto.Nationaloty ?? assessor.User.Nationality;
                assessor.User.day = dto.Day;
                assessor.User.year = dto.Year;
                assessor.User.month = dto.Month;
            }
            try
            {
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    Data = true,
                    IsSucceeded = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 500,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> PickSlotAsync(int assessorId, int slotId)
        {
            var assessor = await _context.Assessors.Include(a => a.AvailableSlots).FirstOrDefaultAsync(a => a.Id == assessorId);
            var slot = await _context.Slots.FirstOrDefaultAsync(s => s.Id == slotId);
            if (assessor == null || slot == null)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor or Slot not found"
                };
            }
            if (!assessor.AvailableSlots.Any(s => s.Id == slotId))
            {
                assessor.AvailableSlots.Add(slot);
                await _context.SaveChangesAsync();
            }
            return new ApiResponse<bool>
            {
                Data = true,
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<bool>> RemoveSlotAsync(int assessorId, int slotId)
        {
            var assessor = await _context.Assessors.Include(a => a.AvailableSlots).FirstOrDefaultAsync(a => a.Id == assessorId);
            if (assessor == null)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            var slot = assessor.AvailableSlots.FirstOrDefault(s => s.Id == slotId);
            if (slot == null)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Slot not found in assessor's calendar"
                };
            }
            assessor.AvailableSlots.Remove(slot);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<bool>> DeleteAssessorAsync(int id)
        {
            var assessor = await _context.Assessors.FirstOrDefaultAsync(a => a.Id == id);
            if (assessor == null)
            {
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            _context.Assessors.Remove(assessor);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                IsSucceeded = true,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<IEnumerable<SlotDto>>> GetAvailableSlotsByAssessorIdAsync(int assessorId)
        {
            var assessor = await _context.Assessors
                .Include(a => a.AvailableSlots)
                .FirstOrDefaultAsync(a => a.Id == assessorId);
            if (assessor == null)
            {
                return new ApiResponse<IEnumerable<SlotDto>>
                {
                    IsSucceeded = false,
                    StatusCode = 404,
                    ErrorMessage = "Assessor not found"
                };
            }
            var slotDtos = assessor.AvailableSlots.Select(slot => new SlotDto
            {
                Date = slot.Date,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime
            }).ToList();
            return new ApiResponse<IEnumerable<SlotDto>>
            {
                Data = slotDtos,
                IsSucceeded = true,
                StatusCode = 200
            };
        }
    }
} 