using Microsoft.EntityFrameworkCore;
using Patients.Domain.Entities;

namespace Patients.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Patient> Patients { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}