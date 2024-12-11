using Microsoft.EntityFrameworkCore;
using TodoTestSuite.Components.Services;
using TodoTestSuite.Models;

namespace TodoTestSuite.Database;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> Todos { get; set; }
}