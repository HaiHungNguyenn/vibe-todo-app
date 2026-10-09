var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

// Exposes the top-level-statements Program class so WebApplicationFactory<Program> can reference it from tests.
public partial class Program;
