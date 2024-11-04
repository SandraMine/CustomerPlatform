using Microsoft.EntityFrameworkCore;

namespace CustomerPlatform.Models
{
    public class AuditLogContext : DbContext
    {
        public AuditLogContext(DbContextOptions<AuditLogContext> options) : base(options)
        {

        }

        public DbSet<AuditLog>? AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.Id);
        }
    }
}
