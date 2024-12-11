using TodoTestSuite.Database;
using TodoTestSuite.Models;

namespace TodoTestSuite.Components.Services;

public class TodoDBService(TodoDbContext context)
{
  private readonly TodoDbContext _context = context;

  public TodoItem[] Items => [.. _context.Todos];

  public string Add(string title, bool isDone = false)
  {
    var id = Guid.NewGuid().ToString();
    _context.Todos.Add(new TodoItem(id, title, isDone));

    _context.SaveChanges();
    return id;
  }

  public async Task Check(string id)
  {
    var todo = await _context.Todos.FindAsync(id)
      ?? throw new KeyNotFoundException($"Todo item with id {id} not found");

    _context.Todos.Update(todo with { IsDone = true });
    _context.SaveChanges();
  }
}
