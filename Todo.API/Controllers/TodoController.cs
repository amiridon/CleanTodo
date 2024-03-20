using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;
using Todo.Application.Services.DTO;
using Serilog;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet("gettodos")]
        public async Task<IActionResult> GetTodos()
        {
            _logger.LogInformation("Getting all todos");
            var todos = await _todoService.GetTodoItemsAsync();
            return Ok(todos);
        }

        [HttpGet("gettodo/{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            _logger.LogInformation("Getting todo with id {id}", id);
            var todo = await _todoService.GetTodoItemAsync(id);
            return todo is not null ? Ok(todo) : NotFound();
        }

        [HttpPost("createtodo")]
        public async Task<IActionResult> CreateTodo(TodoItemDto todoItem)
        {
            _logger.LogInformation("Creating new todo");
            var todo = await _todoService.CreateTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("updatetodo/{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, TodoItemDto todoItem)
        {
            _logger.LogInformation("Updating todo with id {id}", id);
            var todo = await _todoService.UpdateTodoItemAsync(id, todoItem);
            if (todo == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}