namespace TodoApi.ToDo_Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public static implicit operator TaskItem(Todo_Domain.Entities.Task task)
        {
            return new TaskItem
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StatusId = task.StatusId,
                ExpirationDate = task.ExpirationDate
            };
        }

        public static implicit operator Todo_Domain.Entities.Task(TaskItem taskItem)
        {
            return new Todo_Domain.Entities.Task(
                taskItem.Title,
                taskItem.Description,
                taskItem.StatusId ?? (int)Todo_Domain.Enums.TaskStatusEnum.Pending,
                taskItem.ExpirationDate ?? DateTime.UtcNow.Date);
        }
    }
}