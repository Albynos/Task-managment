using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class TaskItemDbConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(task => task.Id);
            builder.HasIndex(task => task.Id).IsUnique();
            builder.Property(task => task.Title).HasMaxLength(250);
        }
    }
}