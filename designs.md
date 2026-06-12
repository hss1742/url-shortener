# Architecture Decisions

## Clean Architecture

### Why Clean Architecture?

* Separate business logic from AWS-specific concerns.
* Keep the Domain independent of frameworks and infrastructure.
* Allow Infrastructure implementations to change without affecting use cases.
* Keep Lambda handlers thin and focused on request/response translation.

## Dependency Direction

Project References

Api
├── Application
└── Infrastructure

Infrastructure
├── Application
└── Domain

Application
└── Domain

Domain

### Architectural Rule

Dependencies ultimately point toward the Domain layer.

The Domain layer contains business concepts and has no dependencies on any other project.

The Api project references Infrastructure because it acts as the Composition Root and is responsible for wiring application services and infrastructure implementations.
```

### Key Decisions

* Repository contracts are owned by the Application layer.
* Infrastructure provides implementations of Application contracts.
* Dependency Injection is configured at the Composition Root.
* Domain remains independent of AWS services and persistence technologies.

---

## Dependency Injection & Composition Root

### Why Dependency Injection?

* Decouple use cases from concrete implementations.
* Improve testability.
* Centralize object creation and wiring.

### Key Decisions

* Service registrations are performed during Lambda cold start.
* A single ServiceProvider instance is reused across invocations.
* Application services are resolved through the container.
* Lambda handlers do not manually create dependencies.

---

## Domain Modeling

### Why Model the Domain Before Persistence?

* Business concepts should drive storage design.
* The database schema should adapt to the domain model.
* Prevent infrastructure concerns from leaking into business logic.

### Domain vs Application

#### Domain

Represents business concepts and rules.

Examples:

* ShortUrl
* ShortCode
* OriginalUrl

#### Application

Represents use cases.

Examples:

* CreateShortUrl
* GetOriginalUrl
* DeleteShortUrl

### Entity vs Value Object

#### Entity

Has a stable identity.

Example:

* ShortUrl

#### Value Object

Defined entirely by its value.

Future candidates:

* ShortCode
* OriginalUrl

### ShortUrl Entity (V1)

Properties:

* Id
* OriginalUrl
* ShortCode
* CreatedAt

### CreatedAt Decision

Included in the Domain model because future business rules may depend on creation time.

### ClickCount Decision

Excluded initially.

Reasons:

* No current use case requires it.
* Avoid premature optimization.
* Analytics may eventually be stored separately from core URL data.

### Encapsulation Decision

Properties use private setters to prevent uncontrolled mutation and allow business rules to be enforced through domain behavior.
