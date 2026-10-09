using System.Collections.Concurrent;

namespace TodoApi.Features.Todos;

public sealed class InMemoryTodoStore : ITodoStore
{
    private readonly ConcurrentDictionary<int, Todo> _todos = new();
    private int _lastId;

    public IReadOnlyList<Todo> GetAll() => [.. _todos.Values.OrderBy(t => t.Id)];

    public Todo? Get(int id) => _todos.GetValueOrDefault(id);

    public Todo Add(string title)
    {
        var todo = new Todo(Interlocked.Increment(ref _lastId), title, false, DateTimeOffset.UtcNow);
        _todos[todo.Id] = todo;
        return todo;
    }

    public Todo? Update(int id, string title, bool isCompleted)
    {
        while (_todos.TryGetValue(id, out var existing))
        {
            var updated = existing with { Title = title, IsCompleted = isCompleted };
            if (_todos.TryUpdate(id, updated, existing))
            {
                return updated;
            }
        }

        return null;
    }

    public bool Delete(int id) => _todos.TryRemove(id, out _);
}
