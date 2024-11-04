using CustomerPlatform.Models;

namespace CustomerPlatform.Services.Interfaces
{
    
        public interface IAuditLogService
        {
            Task<IEnumerable<AuditLog>> GetAuditLogsAsync();
        }
    }

