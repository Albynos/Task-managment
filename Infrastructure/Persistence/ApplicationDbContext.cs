using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }

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