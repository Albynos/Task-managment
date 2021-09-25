using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        { 
            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var taskItems = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Empty,
                    Title = "Test Name",
                    Description = "Test Desc",
                    Status = TaskStatus.Undone,
                    CreationDate = DateTime.Now,
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Empty,
                    Title = "Test Name 2",
                    Description = "Test Desc 2",
                    Status = TaskStatus.Undone,
                    CreationDate = DateTime.Now,
                },
            };

            modelBuilder.ApplyConfiguration(new TaskItemDbConfiguration());
            modelBuilder.Entity<TaskItem>().HasData(taskItems);
            base.OnModelCreating(modelBuilder);
        }
    }
}