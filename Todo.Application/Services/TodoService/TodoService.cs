using Todo.Domain;

namespace Todo.Application.Services;

public class TodoService : ITodoService
{
    private readonly IEnumerable<TodoItem> _todoItems;

    public TodoService()
    {
        _todoItems = new List<TodoItem>
        {
            new TodoItem { Id = Guid.NewGuid(), Title = "Create a new project", IsDone = true },
            new TodoItem { Id = Guid.NewGuid(), Title = "Add a new feature", IsDone = false },
            new TodoItem { Id = Guid.NewGuid(), Title = "Fix a bug", IsDone = false }
        };
    }

    Task<TodoItem> ITodoService.CreateTodoItemAsync(TodoItem todoItem)
    {
        todoItem.CreateDate = DateTime.Now;
        _todoItems.Append(todoItem);
        return Task.FromResult(todoItem);
    }

    Task ITodoService.DeleteTodoItemAsync(Guid id)
    {
        var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
        if(todoItem is not null)
            _todoItems.ToList().Remove(todoItem);
        return Task.CompletedTask;
    }

    Task<TodoItem?> ITodoService.GetTodoItemAsync(Guid id)
    {
        return Task.FromResult(_todoItems.FirstOrDefault(x => x.Id == id));
    }

    Task<List<TodoItem>> ITodoService.GetTodoItemsAsync()
    {
        return Task.FromResult(_todoItems.ToList());
    }

    Task<TodoItem?> ITodoService.UpdateTodoItemAsync(Guid id, TodoItem todoItem)
    {
        var existingTodoItem = _todoItems.FirstOrDefault(x => x.Id == id);
        if(existingTodoItem is not null)
        {
            existingTodoItem.Title = todoItem.Title;
            existingTodoItem.IsDone = todoItem.IsDone;
            existingTodoItem.ModifiedDate = DateTime.Now;
        }
        return Task.FromResult(existingTodoItem);
    }
}