using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApi.Models;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = "";
    [Required, MaxLength(100)]
    public string Email { get; set; } = "";
    [Required]
    public string PasswordHash { get; set; } = "";
    public List<Workout> Workouts { get; set; } = [];
}