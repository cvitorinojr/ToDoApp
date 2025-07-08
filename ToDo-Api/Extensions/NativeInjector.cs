using ToDo_Domain.Interfaces.Repositories;
using ToDo_Domain.Interfaces.Services;
using ToDo_Repository.Repositories;
using ToDo_Service.Services;

namespace ToDo_Api.Extensions
{
    public static class NativeInjector
    {
        public static void AddLocalServices(this WebApplicationBuilder builder)
        {
            #region Services
            builder.Services.AddScoped<ITaskService, TaskService>();
            #endregion

            #region Repository
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            #endregion
        }
    }
}
