using CustomerPlatform.Models;

namespace CustomerPlatform.Dtos
{
    public class SaveCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ContactInformation ContactInformation { get; set; } = new();
        public decimal? CurrentBalance { get; set; }
    }
}
