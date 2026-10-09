# vibe-todo-app

Todo REST API, built to practice an AI-SDLC loop: one agent codes, another reviews, a human merges.

## Stack
- .NET 10, ASP.NET Core minimal API (`TodoApi/`)
- xUnit tests (`TodoApi.Tests/`), solution file `TodoApi.slnx`
- Storage: in-memory first (`ConcurrentDictionary` behind an `ITodoStore` interface) so it can be swapped for EF Core/SQLite later

## Commands
- Build: `dotnet build TodoApi.slnx`
- Test: `dotnet test TodoApi.slnx`
- Run: `dotnet run --project TodoApi`
- Format check: `dotnet format TodoApi.slnx --verify-no-changes`

A change is not done until build, test, and format check all pass. Run them and report real output; never claim "tests pass" without running them.

## Conventions
- Minimal APIs; group endpoints per feature in `TodoApi/Features/<Feature>/` with a static `Map<Feature>Endpoints(this IEndpointRouteBuilder)` extension. Keep `Program.cs` thin.
- Request/response types are `record`s. Never expose internal entities directly.
- Validate input at the endpoint boundary; return `TypedResults` (`Results<Ok<T>, NotFound>` etc.) and `ProblemDetails` for errors.
- Tests: integration tests via `WebApplicationFactory<Program>` for endpoints, plain unit tests for logic. Every behavior change ships with a test. Name tests `Method_Scenario_Expected`.
- Nullable is on; no `!` suppressions without a comment saying why.
- No new NuGet packages without stating why in the PR.

## Workflow (AI-SDLC)
Two roles, normally two separate Claude sessions so the reviewer has no memory of the coder's reasoning:

1. **Coder** (`se-coder` agent): takes one small task, branches from `main` (`feat/<slug>`, `fix/<slug>`), implements with tests, commits, pushes, opens a PR via the GitHub MCP using `.github/pull_request_template.md`.
2. **Reviewer** (`code-reviewer` agent): reads the PR diff via GitHub MCP, runs build/test locally on the branch, posts a review (`APPROVE` / `REQUEST_CHANGES` / `COMMENT`) with file/line findings.
3. Coder addresses review comments with follow-up commits, never force-push after review starts.
4. **The human merges.** Agents never merge PRs, never push to `main`, never approve their own PR.

Slash commands: `/ship` (coder: finish + open PR), `/review-pr <number>` (reviewer).

Keep PRs small (one behavior, ideally < 300 changed lines). Roadmap lives in `docs/backlog.md`; pick the top unchecked item.

## Safety
- `.env` holds `GITHUB_PAT`; never read, print, or commit it. Use `.env.example` as the template.
- Use `git add <paths>` explicitly, not `git add -A`.
- No destructive git (`reset --hard`, `push --force`, `branch -D`) without being asked.
