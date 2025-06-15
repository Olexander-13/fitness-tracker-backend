using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApi.Models;

public class Workout
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required, MaxLength(50)]
    public string Type { get; set; } = "";
    [Required]
    public int Duration { get; set; }
    [Required]
    public int Calories { get; set; }
    public string? Notes { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}