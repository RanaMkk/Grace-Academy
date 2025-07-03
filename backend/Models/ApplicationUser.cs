using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using backend.Enums;
namespace backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? ImgUrl { get; set; }

        public string? Nationality { get; set; }

        public int day {  get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public Gender Gender { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // to reset the password
        public string? ResetOtpCode { get; set; }
        public DateTime? ResetOtpExpiry { get; set; }

    }
}
