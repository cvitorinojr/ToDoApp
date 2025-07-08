using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.ToDo_Domain.Models;

namespace ToDo_Domain.Interfaces.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAsync(string? status, DateTime? startDate, DateTime? endDate);
        Task CreateAsync(CreateTaskModel taskItem);
        Task UpdateAsync(TaskItem taskItem);
        Task DeleteAsync(int id);
    }
}