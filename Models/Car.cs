using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Make is required.")]
        [StringLength(50, ErrorMessage = "Make can't be longer than 50 characters.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model can't be longer than 50 characters.")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be a valid year.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Price per day is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price per day must be greater than 0.")]
        public decimal PricePerDay { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
