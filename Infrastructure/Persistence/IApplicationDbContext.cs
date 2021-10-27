using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaskItem> TaskItems { get; set; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}