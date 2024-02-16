using Todo.Domain;

namespace Todo.Application.Services;
public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
    Task<TodoItem> GetTodoItemAsync(Guid id);
    Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
    Task<TodoItem> UpdateTodoItemAsync(Guid id, TodoItem todoItem);
    Task DeleteTodoItemAsync(Guid id);
}
