# Architecture Decisions

## Session 6

### Why Clean Architecture?

- Separate business logic from AWS concerns.
- Keep Domain independent.
- Allow Infrastructure implementations to change without affecting use cases.
- Keep Lambda handlers thin.

### Dependency Direction

Api -> Application -> Domain

Infrastructure -> Application -> Domain

### Notes

Health endpoint moved into Application layer to establish the use-case pattern before implementing URL shortening.