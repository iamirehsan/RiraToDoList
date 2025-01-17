namespace RiraToDoList.Domain.Entites;

public class Task : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime DueDate { get; private set; }

    private Task(string title, string description, bool isCompleted, DateTime dueDate) : base()
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        DueDate = dueDate;
    }

    public static Task Create(string title, string description, bool isCompleted, DateTime dueDate)
    {
        return new Task(title, description, isCompleted, dueDate);
    }
    public void Update(string title, string description, bool isCompleted, DateTime dueDate)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        DueDate = dueDate;
    }
}
