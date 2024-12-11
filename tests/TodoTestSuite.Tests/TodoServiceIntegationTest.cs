using AutoFixture.Xunit2;
using TodoTestSuite.Components.Services;
using TodoTestSuite.Tests.fixtures;

namespace TodoTestSuite.Tests;

public class TodoDbServiceTest : DatabaseFixture
{
  private readonly TodoDBService _sut;

  public TodoDbServiceTest() : base()
  {
    _sut = new TodoDBService(context);
  }

  [Fact]
  public async Task TodoService_Add_StoresItemInDatabase()
  {
    // Arrange
    var title = "Test Todo";

    // Act
    var id = _sut.Add(title);

    // Assert
    var todoInDb = await context.Todos.FindAsync(id);
    Assert.NotNull(todoInDb);
    Assert.Equal(title, todoInDb.Title);
  }


  [Theory]
  [AutoData]
  public void TodoService_Items_ReturnsAllItemsFromDatabase(string title)
  {
    // Arrange
    // Act
    _sut.Add(title);

    // Assert
    Assert.Single(context.Todos);
  }
}