---
description: Coder - verify, commit, push, and open a PR for the current branch
---
Finish the current work and open a PR:

1. Confirm we are not on `main`; if so, create a branch first.
2. Run build, test, and format check from CLAUDE.md. Stop and fix if anything fails.
3. Commit remaining changes (explicit paths only), push the branch.
4. Open a PR into `main` via the GitHub MCP using `.github/pull_request_template.md`.
5. Reply with the PR URL and the test results. Do not merge.

Extra context: $ARGUMENTS
