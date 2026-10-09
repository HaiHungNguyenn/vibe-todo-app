using TodoApi.Features.Todos;

namespace TodoApi.Tests;

public class InMemoryTodoStoreTests
{
    private readonly InMemoryTodoStore _store = new();

    [Fact]
    public void Add_NewTitle_ReturnsIncompleteTodoWithId()
    {
        var todo = _store.Add("Buy milk");

        Assert.True(todo.Id > 0);
        Assert.Equal("Buy milk", todo.Title);
        Assert.False(todo.IsCompleted);
    }

    [Fact]
    public void Add_CalledTwice_AssignsDistinctIds()
    {
        var first = _store.Add("a");
        var second = _store.Add("b");

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void Get_ExistingId_ReturnsTodo()
    {
        var added = _store.Add("Buy milk");

        Assert.Equal(added, _store.Get(added.Id));
    }

    [Fact]
    public void Get_UnknownId_ReturnsNull()
    {
        Assert.Null(_store.Get(999));
    }

    [Fact]
    public void GetAll_Empty_ReturnsEmpty()
    {
        Assert.Empty(_store.GetAll());
    }

    [Fact]
    public void GetAll_AfterAdds_ReturnsTodosOrderedById()
    {
        var first = _store.Add("a");
        var second = _store.Add("b");

        Assert.Equal([first, second], _store.GetAll());
    }

    [Fact]
    public void Update_ExistingId_ReplacesTitleAndCompletion()
    {
        var added = _store.Add("old");

        var updated = _store.Update(added.Id, "new", true);

        Assert.NotNull(updated);
        Assert.Equal(added.Id, updated.Id);
        Assert.Equal("new", updated.Title);
        Assert.True(updated.IsCompleted);
        Assert.Equal(updated, _store.Get(added.Id));
    }

    [Fact]
    public void Update_UnknownId_ReturnsNull()
    {
        Assert.Null(_store.Update(999, "x", false));
    }

    [Fact]
    public void Delete_ExistingId_RemovesTodoAndReturnsTrue()
    {
        var added = _store.Add("a");

        Assert.True(_store.Delete(added.Id));
        Assert.Null(_store.Get(added.Id));
    }

    [Fact]
    public void Delete_UnknownId_ReturnsFalse()
    {
        Assert.False(_store.Delete(999));
    }

    [Fact]
    public async Task Add_ConcurrentCalls_AssignsUniqueIds()
    {
        var todos = await Task.WhenAll(Enumerable.Range(0, 100).Select(i => Task.Run(() => _store.Add($"t{i}"))));

        Assert.Equal(100, todos.Select(t => t.Id).Distinct().Count());
        Assert.Equal(100, _store.GetAll().Count);
    }
}
