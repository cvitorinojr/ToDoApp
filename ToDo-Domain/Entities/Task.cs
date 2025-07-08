using Todo_Domain.Enums;

namespace Todo_Domain.Entities;

public class Task
{
    public int Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int StatusId { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public TaskStatus TaskStatus { get; private set; }

    public Task(string title, string description, int statusId, DateTime expirationDate)
    {
        Title = title;
        Description = description;
        StatusId = statusId;
        ExpirationDate = expirationDate;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
    }

    public void UpdateDescription(string description) => Description = description ?? string.Empty;

    public void ChangeStatus(TaskStatusEnum newStatus) => StatusId = (int)newStatus;

    public void SetExpirationDate(DateTime expirationDate)
    {
        if (expirationDate < DateTime.UtcNow)
            throw new ArgumentException("Expiration date cannot be in the past.", nameof(expirationDate));
        ExpirationDate = expirationDate;
    }
}