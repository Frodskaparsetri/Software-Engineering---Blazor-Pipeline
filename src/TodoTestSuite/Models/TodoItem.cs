using Microsoft.EntityFrameworkCore;

namespace TodoTestSuite.Models;

[PrimaryKey(nameof(Id))]
public record TodoItem(string Id, string Title, bool IsDone);
