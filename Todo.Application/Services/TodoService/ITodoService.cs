using Todo.Application.Services.DTO;
using Todo.Domain;

namespace Todo.Application.Services;
public interface ITodoService
{
    Task<List<TodoItem>> GetTodoItemsAsync();
    Task<TodoItem?> GetTodoItemAsync(Guid id);
    Task<TodoItem> CreateTodoItemAsync(TodoItemDto todoItem);
    Task<TodoItem?> UpdateTodoItemAsync(Guid id, TodoItemDto todoItem);
    Task DeleteTodoItemAsync(Guid id);
}
