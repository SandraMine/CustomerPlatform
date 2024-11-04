using CustomerPlatform.Models;
using CustomerPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerPlatform.Services.Providers
{
    public class AuditLogsService : IAuditLogService
    {
        private readonly AuditLogContext _auditLogdbContext;

        public AuditLogsService(AuditLogContext auditLogdbContext)
        {
            _auditLogdbContext = auditLogdbContext;
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
            return await _auditLogdbContext.AuditLogs!.ToListAsync() ?? new List<AuditLog>();
        }
    }
}
