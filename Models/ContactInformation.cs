using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomerPlatform.Models
{
    [Owned]
    public class ContactInformation
    {
        public string Email { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string PrimaryMobileNumber { get; set; } = string.Empty;
        public string SecondaryMobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
