using Todo.Application.Services.DTO;
using Todo.Domain;

namespace Todo.Application.Services;

public class TodoService : ITodoService
{
    private readonly List<TodoItem> _todoItems;

    public TodoService()
    {
        _todoItems = new List<TodoItem>
        {
            new TodoItem { Id = new Guid("c7f10cd3-16d0-44a8-9a97-39d6d07d58ac"), Title = "Create a new project", IsDone = true },
            new TodoItem { Id = new Guid("96eeb547-d1a4-47cf-a80e-da48b2c7deda"), Title = "Add a new feature", IsDone = false },
            new TodoItem { Id = new Guid("ca9c807c-8e2f-4733-b929-d015bbf39cae"), Title = "Fix a bug", IsDone = false }
        };
    }

    Task<TodoItem> ITodoService.CreateTodoItemAsync(TodoItemDto todoItem)
    {
        var newTodoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = todoItem.Title,
            IsDone = todoItem.IsDone,
            CreateDate = DateTime.Now
        };
        _todoItems.Add(newTodoItem);
        return Task.FromResult(newTodoItem);
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

    Task<TodoItem?> ITodoService.UpdateTodoItemAsync(Guid id, TodoItemDto todoItem)
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