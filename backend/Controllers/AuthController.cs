using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using backend.DTOs;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GmailEmailService _emailService;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager, GmailEmailService emailService)
        {
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPost("registerStudent")]
        public async Task<IActionResult> RegisterStudentAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterStudentAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("registerTeacher")]
        public async Task<IActionResult> RegisterTeacherAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterTeacherAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromBody] EmailDto dto)
        {
            string email = dto.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");

            var otp = new Random().Next(100000, 999999).ToString(); // 6-digit OTP
            user.ResetOtpCode = otp;
            user.ResetOtpExpiry = DateTime.UtcNow.AddMinutes(10); // OTP expires in 10 mins
            await _userManager.UpdateAsync(user);

            await _emailService.SendEmailAsync(
                email,
                "Reset Your Password",
                $"Your OTP code is: {otp}. It will expire in 10 minutes."
            );

            return Ok(new ApiResponse<string>
            {
                Data = "OTP sent to your email.",
                IsSucceeded = true,
                StatusCode = 200
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("User not found.");

            if (user.ResetOtpCode != dto.Otp || user.ResetOtpExpiry < DateTime.UtcNow)
                return BadRequest("Invalid or expired OTP.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            user.ResetOtpCode = null;
            user.ResetOtpExpiry = null;
            await _userManager.UpdateAsync(user);
            return Ok(new ApiResponse<string>
            {
                Data = "Password reset successfully.",
                IsSucceeded = true,
                StatusCode = 200
            });
        }

        [HttpPost("registerAssessor")]
        public async Task<IActionResult> RegisterAssessorAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAssessorAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
