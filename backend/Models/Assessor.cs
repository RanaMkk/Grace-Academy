using backend.Enums;
namespace backend.Models
{
    public class Assessor
    {
        public int Id { get; set; }

        // Link to Identity user
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // background info
        public EmploymentType EmploymentType { get; set; }

        public string? Bio { get; set; }

        public string? College { get; set; }
        public string? University { get; set; }

        public int YearsOfExperience { get; set; }

        public string? Specializations { get; set; } // "IELTS,Business English"
        public string? LanguagesSpoken { get; set; } // "English,Arabic"
        public bool IsVerified { get; set; } = false;
        public bool IsNative { get; set; } = false;

        // platform info
        public int CompletedAssessments { get; set; } = 0; // default is 0
        public int PendingAssessments { get; set; } = 0; // default is 0
        public int weekAssessments { get; set; } = 0;  // to be calculated
        public double Rate { get; set; } // to be calculated based on the feedbacks

        // Many-to-many relation
        public ICollection<Slot> AvailableSlots { get; set; } = new List<Slot>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
