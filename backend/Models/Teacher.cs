using backend.Enums;

namespace backend.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        // Link to Identity user
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Bio { get; set; }
        public double Rate { get; set; }
        public int ReviewCount { get; set; }
        public int StudentCount { get; set; }
        public int CourseCount { get; set; }
        public int YearsOfExperience { get; set; }
        public string University { get; set; }
        public string College { get; set; }
        public EmploymentType EmploymentType { get; set; }
    }
} 