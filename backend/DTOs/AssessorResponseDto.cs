namespace backend.DTOs
{
    public class AssessorResponseDto
    {
        public int AssessorID { get; set; }
        public string UserID { get; set; }
        public string? Bio { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Collage { get; set; }
        public string? Uni { get; set; }
        public int YearsOfExp { get; set; }
        public string? Specilization { get; set; }
        public string? Langugues { get; set; }
        public bool IsNative { get; set; }
        public string? ImgUrl { get; set; }
        public string? Nationaloty { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
} 