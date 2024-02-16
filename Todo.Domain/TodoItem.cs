namespace Todo.Domain;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public DateTime DeletedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}