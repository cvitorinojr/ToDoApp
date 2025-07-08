using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo_Domain.Interfaces.Repositories;
using ToDo_Repository.Context;

namespace ToDo_Repository.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DefaultContext _context;

        public TaskRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo_Domain.Entities.Task>> GetAsync(DateTime? startDate = null, DateTime? endDate = null, string? status = null)
        {
            IQueryable<Todo_Domain.Entities.Task> query = _context.Set<Todo_Domain.Entities.Task>();

            query = query.Where(t => (!startDate.HasValue || t.ExpirationDate >= startDate.Value)
                                && (!endDate.HasValue || t.ExpirationDate <= endDate.Value));

            if (int.TryParse(status, out int statusId))
                query = query.Where(t => t.StatusId == statusId);

            return await query.ToListAsync();
        }

        public async Task AddAsync(Todo_Domain.Entities.Task task)
        {
            await _context.Set<Todo_Domain.Entities.Task>().AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo_Domain.Entities.Task task)
        {
            _context.Set<Todo_Domain.Entities.Task>().Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Set<Todo_Domain.Entities.Task>().FindAsync(id);
            if (task != null)
            {
                _context.Set<Todo_Domain.Entities.Task>().Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Todo_Domain.Entities.Task?> GetByIdAsync(int id)
        {
            return _context.Set<Todo_Domain.Entities.Task>().FindAsync(id).AsTask();
        }
    }
}