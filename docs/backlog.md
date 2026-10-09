# Backlog

Pick the top unchecked item; one PR each.

- [ ] Make `Program` testable (`public partial class Program`), add `WebApplicationFactory` smoke test for `GET /`
- [ ] Todo model + `ITodoStore` in-memory implementation (with unit tests)
- [ ] `POST /todos` (validate non-empty title, max 200 chars) -> 201 + Location
- [ ] `GET /todos` and `GET /todos/{id}` (404 on unknown id)
- [ ] `PUT /todos/{id}` and `PATCH /todos/{id}/complete`
- [ ] `DELETE /todos/{id}`
- [ ] Filtering: `GET /todos?completed=true`
- [ ] OpenAPI doc + Swagger UI in Development
- [ ] GitHub Actions CI: build, test, format check on PRs
- [ ] Persistence with EF Core + SQLite
