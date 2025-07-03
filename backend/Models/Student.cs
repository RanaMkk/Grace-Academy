using backend.Enums;

namespace backend.Models
{
    public class Student
    {
        public int Id { get; set; }
        // Link to Identity user
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public EnglishLevel EnglishLevel { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public int CoursesCompleted { get; set; }
        public string AssessmentFeedback { get; set; }
        public bool IsAssessed { get; set; }
        public int EnrolledCourses { get; set; }
    }
} 