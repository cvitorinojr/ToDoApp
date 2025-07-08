using Microsoft.EntityFrameworkCore;
using Todo_Domain.Entities;

namespace ToDo_Repository.Context
{
    public class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options)
    {
        public DbSet<Todo_Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Todo_Domain.Entities.TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);

        }
    }
}