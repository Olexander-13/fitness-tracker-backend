using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApi.Models.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = "";

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = "";
    }
}