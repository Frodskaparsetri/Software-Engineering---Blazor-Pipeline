using TodoTestSuite.Models;

namespace TodoTestSuite.Components.Services;

public class TodoService
{
  private readonly Dictionary<string, TodoItem> _todos = [];

  public string Add(string title, bool isDone = false)
  {
    var id = Guid.NewGuid().ToString();
    _todos.Add(id, new TodoItem(id, title, isDone));
    return id;
  }

  public void Check(string id)
  {
    if (!_todos.TryGetValue(id, out var todo))
    {
      throw new KeyNotFoundException($"Todo item with id {id} not found");
    }

    _todos[id] = todo with { IsDone = true };
  }

  public TodoItem[] Items => [.. _todos.Values];
}
