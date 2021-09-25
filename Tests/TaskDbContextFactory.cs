using System;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public static class TaskDbContextFactory
    {
        public static Guid FirstTaskId = Guid.NewGuid();
        public static Guid SecondTaskId = Guid.NewGuid();

        public static Guid FirstUserId = Guid.NewGuid();
        public static Guid SecondUserId = Guid.NewGuid();
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.TaskItems.AddRange(
                new TaskItem
                {
                    Id = FirstTaskId,
                    UserId = FirstUserId,
                    Title = "Test Name",
                    Description = "Test Desc",
                    Status = TaskStatus.Undone,
                    CreationDate = DateTime.Now,
                },
                new TaskItem
                {
                    Id = SecondTaskId,
                    UserId = SecondUserId,
                    Title = "Test Name 2",
                    Description = "Test Desc 2",
                    Status = TaskStatus.Undone,
                    CreationDate = DateTime.Now,
                });
            context.SaveChangesAsync();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}