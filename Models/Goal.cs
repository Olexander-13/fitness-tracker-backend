using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTrackerApi.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WeeklyDuration { get; set; } // in minutes

        [Required]
        public int WeeklyCalories { get; set; } // in kcal

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}