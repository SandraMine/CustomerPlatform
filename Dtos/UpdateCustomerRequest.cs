using CustomerPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerPlatform.Dtos
{
    public class UpdateCustomerRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ContactInformation ContactInformation { get; set; } = new();
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current Balance must be a non-negative value.")]
        public decimal? CurrentBalance { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
