namespace Exp.TodoApp.Domain.Entities;

public class Todo
{
    [Key]
    public int Id { get; set; }
    public string? ToDoName { get; set; }
}
