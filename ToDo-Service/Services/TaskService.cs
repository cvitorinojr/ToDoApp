using System;
using System.Collections.Generic;
using System.Linq;
using ToDo_Domain.Interfaces.Repositories;
using ToDo_Domain.Interfaces.Services;
using TodoApi.ToDo_Domain.Models;

namespace ToDo_Service.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        public async Task CreateAsync(CreateTaskModel taskItem)
        {
            if (taskItem == null)
                throw new ArgumentNullException(nameof(taskItem));

            if (string.IsNullOrWhiteSpace(taskItem.Title))
                throw new ArgumentException("Title is required.", nameof(taskItem.Title));

            if (taskItem.ExpirationDate < DateTime.UtcNow.Date)
                throw new ArgumentException("ExpirationDate date invalid.", nameof(taskItem.ExpirationDate));

            await taskRepository.AddAsync((Todo_Domain.Entities.Task)taskItem);
        }
        public async Task DeleteAsync(int id)
        {
            var existingTask = await taskRepository.GetByIdAsync(id);
            if (existingTask == null)
            throw new InvalidOperationException("Task not found.");

            await taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAsync(string? status, DateTime? startDate, DateTime? endDate)
        {
            var tasks = await taskRepository.GetAsync(startDate, endDate, status);
            return tasks.Select(t => (TaskItem)t);
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            if (taskItem == null)
            throw new ArgumentNullException(nameof(taskItem));

            var existingTask = await taskRepository.GetByIdAsync(taskItem.Id)
            ?? throw new InvalidOperationException("Task not found.");

            if (!string.IsNullOrWhiteSpace(taskItem.Title))
            existingTask.UpdateTitle(taskItem.Title);

            if (taskItem.Description != null)
            existingTask.UpdateDescription(taskItem.Description);

            if (taskItem.StatusId >= 1 && taskItem.StatusId <= 3)
            existingTask.ChangeStatus((Todo_Domain.Enums.TaskStatusEnum)taskItem.StatusId);

            if (taskItem.ExpirationDate.HasValue)
            existingTask.SetExpirationDate(taskItem.ExpirationDate.Value);

            await taskRepository.UpdateAsync(existingTask);
        }
    }
}