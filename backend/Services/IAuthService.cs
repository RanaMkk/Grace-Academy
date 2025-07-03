using backend.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RegisterStudentAsync(RegisterModel model);
        Task<AuthModel> RegisterTeacherAsync(RegisterModel model);
        Task<AuthModel> RegisterAssessorAsync(RegisterModel model);
    }
}
