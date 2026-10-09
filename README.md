# vibe-todo-app

Todo REST API (.NET 10 minimal API) built to practice an AI-SDLC loop: a coder agent opens PRs, a reviewer agent reviews them, a human merges.

## Setup
1. `cp .env.example .env` and add a fine-grained GitHub PAT (this repo only; Contents, Pull requests, Issues: read/write).
2. Load `.env` into your shell, then launch `claude` (the GitHub MCP in `.mcp.json` reads `GITHUB_PAT` from the environment).
3. Run two sessions: `claude --agent se-coder` for coding, `claude --agent code-reviewer` for review (or use `/ship` and `/review-pr <n>`).

See [CLAUDE.md](CLAUDE.md) for workflow and conventions, [docs/backlog.md](docs/backlog.md) for what to build.
