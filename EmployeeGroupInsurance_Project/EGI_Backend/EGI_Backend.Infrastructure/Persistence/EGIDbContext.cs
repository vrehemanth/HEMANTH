using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EGI_Backend.Infrastructure.Persistence
{
    public class EGIDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EGIDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CorporateClient> CorporateClients { get; set; }
        public DbSet<CorporateClientDocument> CorporateClientDocuments { get; set; }
        public DbSet<PolicyAssignment> PolicyAssignments { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimDocument> ClaimDocuments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<PlanCoverage> PlanCoverages { get; set; }
        public DbSet<AgentCustomer> AgentCustomers { get; set; }
        public DbSet<PolicyEndorsement> PolicyEndorsements { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(auditEntries, cancellationToken);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            var user = _httpContextAccessor?.HttpContext?.User;
            var userId = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value 
                         ?? user?.FindFirst("sub")?.Value 
                         ?? "System";

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.EntityName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.EntityId = property.CurrentValue?.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.Entry.Properties.Any(p => p.Metadata.IsPrimaryKey())))
            {
                AuditLogs.Add(auditEntry.ToAuditLog());
            }

            return auditEntries.Where(_ => _.Entry.Properties.Any(p => p.Metadata.IsPrimaryKey())).ToList();
        }

        private async Task OnAfterSaveChanges(List<AuditEntry> auditEntries, CancellationToken cancellationToken = default)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.Entry.Properties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.EntityId = prop.CurrentValue?.ToString();
                    }
                }
                AuditLogs.Add(auditEntry.ToAuditLog());
            }

            await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<AuditLog>()
                .HasIndex(al => al.Timestamp);

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.ClaimNumber)
                .IsUnique();

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.Status);

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.ClaimDate);

            modelBuilder.Entity<PolicyAssignment>()
                .HasIndex(pa => pa.PolicyNo)
                .IsUnique();

            modelBuilder.Entity<PolicyAssignment>()
                .HasIndex(pa => pa.Status);

            modelBuilder.Entity<PolicyAssignment>()
                .HasIndex(pa => pa.CorporateClientId);

            modelBuilder.Entity<PolicyAssignment>()
                .HasIndex(pa => pa.AgentId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.InvoiceNo)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.Status);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.PolicyAssignmentId);

            modelBuilder.Entity<Member>()
                .HasIndex(m => new { m.EmployeeCode, m.CorporateClientId })
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.PolicyAssignmentId);

            modelBuilder.Entity<AgentCustomer>()
                .HasIndex(ac => ac.AgentId);

            modelBuilder.Entity<AgentCustomer>()
                .HasIndex(ac => ac.CorporateClientId);

            // Additional indexes for common query fields
            modelBuilder.Entity<CorporateClient>()
                .HasIndex(cc => cc.CompanyName);

            modelBuilder.Entity<CorporateClient>()
                .HasIndex(cc => cc.UserId);

            // Removed invalid index on Member.CorporateClientId as it doesn't exist on the entity.
            // PolicyAssignmentId is already indexed.

            modelBuilder.Entity<Dependent>()
                .HasIndex(d => d.MemberId);

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.MemberId);

            modelBuilder.Entity<Claim>()
                .HasIndex(c => c.DependentId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.DueDate);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.InvoiceId);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.PaidBy);

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.UserId);

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.IsRead);

            modelBuilder.Entity<CorporateClient>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.CompanyName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(c => c.Address)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.HasOne(c => c.User)
                      .WithOne()
                      .HasForeignKey<CorporateClient>(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PolicyEndorsement>(entity =>
            {
                entity.HasKey(pe => pe.Id);
                entity.Property(pe => pe.PremiumAdjustment).HasPrecision(18, 2);
                entity.Property(pe => pe.CommissionAdjustment).HasPrecision(18, 2);

                entity.HasOne(pe => pe.RequestedByUser)
                      .WithMany()
                      .HasForeignKey(pe => pe.RequestedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pe => pe.ReviewedByUser)
                      .WithMany()
                      .HasForeignKey(pe => pe.ReviewedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CorporateClientDocument>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.FileName)
                      .IsRequired()
                      .HasMaxLength(255);
                entity.Property(d => d.FilePath)
                      .IsRequired()
                      .HasMaxLength(500);
                entity.HasOne(d => d.CorporateClient)
                      .WithMany(c => c.Documents)
                      .HasForeignKey(d => d.CorporateClientId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PlanCoverage>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.HasOne(pc => pc.InsurancePlan)
                      .WithMany(ip => ip.Coverages)
                      .HasForeignKey(pc => pc.InsurancePlanId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AgentCustomer>(entity =>
            {
                entity.HasKey(ac => ac.Id);
                
                entity.HasOne(ac => ac.Agent)
                      .WithMany()
                      .HasForeignKey(ac => ac.AgentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ac => ac.CorporateClient)
                      .WithMany()
                      .HasForeignKey(ac => ac.CorporateClientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PolicyAssignment>(entity =>
            {
                entity.HasKey(pa => pa.Id);
                
                entity.HasOne(pa => pa.CorporateClient)
                      .WithMany()
                      .HasForeignKey(pa => pa.CorporateClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pa => pa.InsurancePlan)
                      .WithMany()
                      .HasForeignKey(pa => pa.InsurancePlanId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pa => pa.Agent)
                      .WithMany()
                      .HasForeignKey(pa => pa.AgentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);
                
                entity.HasOne(m => m.PolicyAssignment)
                      .WithMany(pa => pa.Members)
                      .HasForeignKey(m => m.PolicyAssignmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.CorporateClient)
                      .WithMany()
                      .HasForeignKey(m => m.CorporateClientId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.HasKey(d => d.Id);
                
                entity.HasOne(d => d.Member)
                      .WithMany(m => m.Dependents)
                      .HasForeignKey(d => d.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.PolicyAssignment)
                      .WithMany()
                      .HasForeignKey(c => c.PolicyAssignmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Member)
                      .WithMany()
                      .HasForeignKey(c => c.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Dependent)
                      .WithMany()
                      .HasForeignKey(c => c.DependentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.ReviewedByUser)
                      .WithMany()
                      .HasForeignKey(c => c.ReviewedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClaimDocument>(entity =>
            {
                entity.HasKey(cd => cd.Id);

                entity.HasOne(cd => cd.Claim)
                      .WithMany(c => c.Documents)
                      .HasForeignKey(cd => cd.ClaimId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.HasOne(i => i.PolicyAssignment)
                      .WithMany()
                      .HasForeignKey(i => i.PolicyAssignmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(i => i.Amount).HasPrecision(18, 2);
                entity.Property(i => i.TotalPaid).HasPrecision(18, 2);
                entity.Property(i => i.CommissionEarned).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.Invoice)
                      .WithMany(i => i.Payments)
                      .HasForeignKey(p => p.InvoiceId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.PaidByUser)
                      .WithMany()
                      .HasForeignKey(p => p.PaidBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.PaidAmount).HasPrecision(18, 2);
            });

            // Fix decimal precision warnings
            modelBuilder.Entity<InsurancePlan>(entity =>
            {
                entity.Property(p => p.BasePremium).HasPrecision(18, 2);
            });

            modelBuilder.Entity<PlanCoverage>(entity =>
            {
                entity.Property(p => p.CoverageAmount).HasPrecision(18, 2);
            });

            // Seed 15 Insurance Plans
            modelBuilder.Entity<InsurancePlan>().HasData(
                // SMALL (10-50 Employees)
                new InsurancePlan { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), PlanCode = "S-Level 1", PlanName = "Health Basic", BasePremium = 2000, Description = "Health: ₹2L | Covers: Employee Only | No Life | No Accident", Status = true },
                new InsurancePlan { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), PlanCode = "S-Level 2", PlanName = "Health Family", BasePremium = 4000, Description = "Health: ₹3L | Covers: Employee + Spouse + 2 Children | No Life | No Accident", Status = true },
                new InsurancePlan { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), PlanCode = "S-Level 3", PlanName = "Health + Life", BasePremium = 6000, Description = "Health: ₹3L (Family) | Life: ₹5L | No Accident | Parents Not Included", Status = true },

                // MEDIUM (50-250 Employees)
                new InsurancePlan { Id = Guid.Parse("22222222-2222-2222-2222-222222222221"), PlanCode = "M-Level 1", PlanName = "Health Basic", BasePremium = 2500, Description = "Health: ₹3L | Covers: Employee Only", Status = true },
                new InsurancePlan { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), PlanCode = "M-Level 2", PlanName = "Health Family", BasePremium = 5000, Description = "Health: ₹5L | Covers: Employee + Spouse + 2 Children", Status = true },
                new InsurancePlan { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), PlanCode = "M-Level 3", PlanName = "Health + Life", BasePremium = 8000, Description = "Health: ₹5L (Family) | Life: ₹10L | Parents Not Included", Status = true },
                new InsurancePlan { Id = Guid.Parse("22222222-2222-2222-2222-222222222224"), PlanCode = "M-Level 4", PlanName = "Health + Life + Accident", BasePremium = 10000, Description = "Health: ₹7L (Family) | Life: ₹15L | Accident: ₹10L | Parents Optional Add-on", Status = true },

                // LARGE (250-1000 Employees)
                new InsurancePlan { Id = Guid.Parse("33333333-3333-3333-3333-333333333331"), PlanCode = "L-Level 1", PlanName = "Health Basic", BasePremium = 4000, Description = "Health: ₹5L | Covers: Employee Only", Status = true },
                new InsurancePlan { Id = Guid.Parse("33333333-3333-3333-3333-333333333332"), PlanCode = "L-Level 2", PlanName = "Health Family", BasePremium = 7000, Description = "Health: ₹7L | Covers: Employee + Spouse + 2 Children", Status = true },
                new InsurancePlan { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), PlanCode = "L-Level 3", PlanName = "Health + Life", BasePremium = 12000, Description = "Health: ₹7L (Family) | Life: ₹20L", Status = true },
                new InsurancePlan { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), PlanCode = "L-Level 4", PlanName = "Health + Life + Accident", BasePremium = 16000, Description = "Health: ₹10L (Family) | Life: ₹30L | Accident: ₹20L", Status = true },
                new InsurancePlan { Id = Guid.Parse("33333333-3333-3333-3333-333333333335"), PlanCode = "L-Level 5", PlanName = "Comprehensive Plus", BasePremium = 25000, Description = "Health: ₹15L (Employee + Spouse + 2 Children + Parents) | Life: ₹40L | Accident: ₹30L | Critical Illness Included", Status = true },

                // ENTERPRISE (1000+ Employees)
                new InsurancePlan { Id = Guid.Parse("44444444-4444-4444-4444-444444444441"), PlanCode = "E-Level 1", PlanName = "Health Family", BasePremium = 6000, Description = "Health: ₹7L | Covers: Employee + Spouse + 2 Children", Status = true },
                new InsurancePlan { Id = Guid.Parse("44444444-4444-4444-4444-444444444442"), PlanCode = "E-Level 2", PlanName = "Health + Life", BasePremium = 14000, Description = "Health: ₹10L (Employee + Spouse + 2 Children) | Life: ₹30L", Status = true },
                new InsurancePlan { Id = Guid.Parse("44444444-4444-4444-4444-444444444443"), PlanCode = "E-Level 3", PlanName = "Full Corporate Shield", BasePremium = 30000, Description = "Health: ₹20L (Employee + Spouse + 2 Children + Parents) | Life: ₹50L | Accident: ₹40L | Global Coverage Option | Dedicated Claim Officer", Status = true }
            );

            // Seed Plan Coverages
            modelBuilder.Entity<PlanCoverage>().HasData(
                // SMALL
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Type = CoverageType.Health, CoverageAmount = 200000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("11111111-1111-1111-1111-111111111112"), Type = CoverageType.Health, CoverageAmount = 300000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("11111111-1111-1111-1111-111111111113"), Type = CoverageType.Health, CoverageAmount = 300000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("11111111-1111-1111-1111-111111111113"), Type = CoverageType.Life, CoverageAmount = 500000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                // MEDIUM
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222221"), Type = CoverageType.Health, CoverageAmount = 300000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Type = CoverageType.Health, CoverageAmount = 500000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222223"), Type = CoverageType.Health, CoverageAmount = 500000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222223"), Type = CoverageType.Life, CoverageAmount = 1000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222224"), Type = CoverageType.Health, CoverageAmount = 700000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222224"), Type = CoverageType.Life, CoverageAmount = 1500000, CoveredGroup = CoveredGroup.EmployeeOnly },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("22222222-2222-2222-2222-222222222224"), Type = CoverageType.Accident, CoverageAmount = 1000000, CoveredGroup = CoveredGroup.EmployeeOnly },

                // LARGE
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333331"), Type = CoverageType.Health, CoverageAmount = 500000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333332"), Type = CoverageType.Health, CoverageAmount = 700000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Type = CoverageType.Health, CoverageAmount = 700000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Type = CoverageType.Life, CoverageAmount = 2000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333334"), Type = CoverageType.Health, CoverageAmount = 1000000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333334"), Type = CoverageType.Life, CoverageAmount = 3000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333334"), Type = CoverageType.Accident, CoverageAmount = 2000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333335"), Type = CoverageType.Health, CoverageAmount = 1500000, CoveredGroup = CoveredGroup.EmployeeFamilyAndParents },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333335"), Type = CoverageType.Life, CoverageAmount = 4000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333335"), Type = CoverageType.Accident, CoverageAmount = 3000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("33333333-3333-3333-3333-333333333335"), Type = CoverageType.CriticalIllness, CoverageAmount = 500000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                // ENTERPRISE
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444441"), Type = CoverageType.Health, CoverageAmount = 700000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444442"), Type = CoverageType.Health, CoverageAmount = 1000000, CoveredGroup = CoveredGroup.EmployeeAndFamily },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444442"), Type = CoverageType.Life, CoverageAmount = 3000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444443"), Type = CoverageType.Health, CoverageAmount = 2000000, CoveredGroup = CoveredGroup.EmployeeFamilyAndParents },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444443"), Type = CoverageType.Life, CoverageAmount = 5000000, CoveredGroup = CoveredGroup.EmployeeOnly },
                new PlanCoverage { Id = Guid.NewGuid(), InsurancePlanId = Guid.Parse("44444444-4444-4444-4444-444444444443"), Type = CoverageType.Accident, CoverageAmount = 4000000, CoveredGroup = CoveredGroup.EmployeeOnly }
            );
        }
    }
}