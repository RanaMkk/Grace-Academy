using System;

namespace backend.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Duration { get; set; } // Duration in mins
        
        // Many-to-many relathinship with the assessor 
        public ICollection<Assessor> Assessors { get; set; } = new List<Assessor>();
    }
} 