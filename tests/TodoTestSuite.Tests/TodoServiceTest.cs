using AutoFixture.Xunit2;
using TodoTestSuite.Components.Services;

namespace TodoTestSuite.Tests;

// hello world

public class TodoServiceTest
{
    [Fact]
    public void TodoService_Items_IsEmptyByDefault()
    {
        // Arrange
        // Act
        var sut = new TodoService();

        // Assert
        Assert.Empty(sut.Items);
    }

    [Theory]
    [AutoData]
    public void TodoService_Add_IncreasesItemsCount(string title)
    {
        // Arrange
        var sut = new TodoService();

        // Act
        sut.Add(title);

        // Assert
        Assert.Single(sut.Items);
    }
}