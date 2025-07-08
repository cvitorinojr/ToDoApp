using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo_Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Todo_Domain.Entities.Task>> GetAsync(DateTime? startDate = null, DateTime? endDate = null, string? status = null);
        Task<Todo_Domain.Entities.Task?> GetByIdAsync(int id);
        Task AddAsync(Todo_Domain.Entities.Task task);
        Task UpdateAsync(Todo_Domain.Entities.Task task);
        Task DeleteAsync(int id);
    }
}