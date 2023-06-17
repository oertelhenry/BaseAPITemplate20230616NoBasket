using Microsoft.EntityFrameworkCore;
using Domain.Odyssey.Entities.Documents;
using Core.Entities.Database;
using Infrastructure.Data.Dtos;

namespace Mobalyz.Odyssey.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
 
        public DbSet<PdfTemplate> PdfTemplate { get; set; }
        public DbSet<HtmlMailTemplate> HtmlMailTemplate { get; set; }

        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<NetworkTrace> NetworkTrace { get; set; }


        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            try
            {
            OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();
            return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
           
        }
        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureOneToOneEntities(modelBuilder);
            this.ConfigureRelationships(modelBuilder);
        }

        protected void ConfigureOneToOneEntities(ModelBuilder modelBuilder)
        {

        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            #region doc

          

            #endregion doc
        }
    }
}
