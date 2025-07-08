namespace Todo_Domain.Entities;

public class TaskStatus
{
    public int Id { get; init; }
    public string Name { get; init; }
    public ICollection<Task> Tasks { get; init; } = new List<Task>();
}