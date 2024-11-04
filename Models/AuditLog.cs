namespace CustomerPlatform.Models
{
    public class AuditLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Action { get; set; } = string.Empty;
        public DateTime? LogDate { get; set; }
    }
}
