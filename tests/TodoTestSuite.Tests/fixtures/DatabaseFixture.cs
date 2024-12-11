


using Microsoft.EntityFrameworkCore;
using TodoTestSuite.Database;
using TodoTestSuite.Tests.utility;

namespace TodoTestSuite.Tests.fixtures;

public abstract class DatabaseFixture : IAsyncLifetime
{
  private static readonly string CONNECTION_STRING = "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=postgres";


  protected readonly TodoDbContext context;

  protected DatabaseFixture()
  {
    var options = new DbContextOptionsBuilder<TodoDbContext>()
        .UseNpgsql(CONNECTION_STRING)
        .Options;

    context = new TodoDbContext(options);
  }

  public async Task InitializeAsync()
  {
    DockerComposer.Up();
    await ResetDatabaseAsync();
  }

  public async Task DisposeAsync()
  {
    await context.DisposeAsync();
    DockerComposer.Down();
  }

  public async Task ResetDatabaseAsync()
  {
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
  }
}
