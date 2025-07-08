using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo_Domain.Entities;
using Todo_Domain.Enums;

namespace ToDo_Repository.Mapping
{
    public class TaskStatusMapping : IEntityTypeConfiguration<Todo_Domain.Entities.TaskStatus>
    {
        public void Configure(EntityTypeBuilder<Todo_Domain.Entities.TaskStatus> builder)
        {
            builder.ToTable("TaskStatus");

            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ts => ts.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(ts => ts.Tasks)
                .WithOne(t => t.TaskStatus)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Todo_Domain.Entities.TaskStatus { Id = (int)TaskStatusEnum.Pending, Name = TaskStatusEnum.Pending.ToString() },
                new Todo_Domain.Entities.TaskStatus { Id = (int)TaskStatusEnum.InProgress, Name = TaskStatusEnum.InProgress.ToString() },
                new Todo_Domain.Entities.TaskStatus { Id = (int)TaskStatusEnum.Done, Name = TaskStatusEnum.Done.ToString() }
            );
        }
    }
}