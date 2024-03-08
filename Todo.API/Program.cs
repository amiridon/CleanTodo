using Todo.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.DTO;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/gettodos", async ([FromServices] ITodoService todoService) =>
{
    Log.Information("Getting all todos");
    var todos = await todoService.GetTodoItemsAsync();
    return Results.Ok(todos);
})
.WithName("GetTodos")
.WithOpenApi();

app.MapGet("/gettodo/{id}", async ([FromServices] ITodoService todoService, Guid id) =>
{
    Log.Information("Getting todo with id {id}", id);
    var todo = await todoService.GetTodoItemAsync(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
})
.WithName("GetTodo")
.WithOpenApi();

app.MapPost("/createtodo", async ([FromServices] ITodoService todoService, TodoItemDto todoItem) =>
{
    Log.Information("Creating new todo");
    var todo = await todoService.CreateTodoItemAsync(todoItem);
    return Results.Created($"/gettodo/{todo.Id}", todo);
});

app.MapPut("/updatetodo/{id}", async ([FromServices] ITodoService todoService, Guid id, TodoItemDto todoItem) =>
{
    Log.Information("Updating todo with id {id}", id);
    var todo = await todoService.UpdateTodoItemAsync(id, todoItem);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

app.MapDelete("/deletetodo/{id}", async ([FromServices] ITodoService todoService, Guid id) =>
{
    Log.Information("Deleting todo with id {id}", id);
    await todoService.DeleteTodoItemAsync(id);
    return Results.NoContent();
});

app.Run();

