using Microsoft.EntityFrameworkCore;
using Patients.Application.Interfaces;
using Patients.Domain.Entities;

namespace Patients.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Patient> Patients { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
