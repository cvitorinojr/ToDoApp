using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo_Repository.Mapping
{
    public class TaskMapping : IEntityTypeConfiguration<Todo_Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Todo_Domain.Entities.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.StatusId)
                .IsRequired();

            builder.Property(t => t.ExpirationDate)
                .IsRequired();

            builder.HasOne(t => t.TaskStatus)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}