using Microsoft.EntityFrameworkCore;

namespace CustomerPlatform.Models
{
    [Owned]
    public class ContactInformation
    {
        public string Email { get; set; } = string.Empty;
        public string PrimaryMobileNumber { get; set; } = string.Empty;
        public string SecondaryMobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
