using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ToDo_Repository.Context
{
    public class DefaultContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultContext>();

            optionsBuilder.UseInMemoryDatabase("ToDoDb");

            return new DefaultContext(optionsBuilder.Options);
        }
    }
}