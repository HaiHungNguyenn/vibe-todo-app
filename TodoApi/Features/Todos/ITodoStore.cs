namespace TodoApi.Features.Todos;

/// <summary>
/// Storage for todos. Callers are responsible for validating input; the store trusts its arguments.
/// </summary>
public interface ITodoStore
{
    /// <summary>Returns all todos ordered by id.</summary>
    IReadOnlyList<Todo> GetAll();

    /// <summary>Returns the todo with the given id, or <c>null</c> if it does not exist.</summary>
    Todo? Get(int id);

    /// <summary>Creates an incomplete todo with a newly assigned id.</summary>
    Todo Add(string title);

    /// <summary>Replaces title and completion of an existing todo; returns <c>null</c> if the id is unknown.</summary>
    Todo? Update(int id, string title, bool isCompleted);

    /// <summary>Removes the todo; returns <c>false</c> if the id is unknown.</summary>
    bool Delete(int id);
}
