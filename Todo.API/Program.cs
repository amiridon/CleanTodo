using Todo.Application.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/gettodos", async ([FromServices] ITodoService todoService) =>
{
    var todos = await todoService.GetTodoItemsAsync();
    return Results.Ok(todos);
})
.WithName("GetTodos")
.WithOpenApi();

app.Run();

