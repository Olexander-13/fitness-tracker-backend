using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApi.Models.Dto
{
    public class UpdateNameDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";
    }
}