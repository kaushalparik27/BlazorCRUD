using BlazorCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext>? _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeeCode).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>().Property(e => e.BasicSalary).HasColumnType("decimal(18,2)");
        }

        public override int SaveChanges()
        {
            _logger?.LogDebug("SaveChanges - Persisting changes to the database.");
            var result = base.SaveChanges();
            _logger?.LogDebug("SaveChanges - {AffectedRows} row(s) affected.", result);
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger?.LogDebug("SaveChangesAsync - Persisting changes to the database.");
            var result = await base.SaveChangesAsync(cancellationToken);
            _logger?.LogDebug("SaveChangesAsync - {AffectedRows} row(s) affected.", result);
            return result;
        }
    }
}