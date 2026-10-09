namespace TodoApi.Features.Todos;

public sealed record Todo(int Id, string Title, bool IsCompleted, DateTimeOffset CreatedAt);
