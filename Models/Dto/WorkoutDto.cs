using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApi.Models.Dto
{
    public class WorkoutDto
    {
        [Required]
        public DateTime Date { get; set; }
        [Required, MaxLength(50)]
        public string Type { get; set; } = "";
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Calories { get; set; }
        public string? Notes { get; set; }
    }
}