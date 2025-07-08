using Todo_Domain.Enums;

namespace TodoApi.ToDo_Domain.Models
{
    public class CreateTaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }

        public static implicit operator CreateTaskModel(Todo_Domain.Entities.Task task)
        {
            return new CreateTaskModel
            {
                Title = task.Title,
                Description = task.Description,
                ExpirationDate = task.ExpirationDate
            };
        }

        public static implicit operator Todo_Domain.Entities.Task(CreateTaskModel taskItem)
        {
            return new Todo_Domain.Entities.Task(
                taskItem.Title,
                taskItem.Description,
                (int)TaskStatusEnum.Pending,
                taskItem.ExpirationDate);
        }
    }
}