# Architecture Decisions

## Clean Architecture

### Why Clean Architecture?

* Separate business logic from AWS-specific concerns.
* Keep the Domain independent of frameworks and infrastructure.
* Allow Infrastructure implementations to change without affecting use cases.
* Keep Lambda handlers thin and focused on request/response translation.

## Dependency Direction

### Project References

```text
Api
├── Application
└── Infrastructure

Infrastructure
├── Application
└── Domain

Application
└── Domain

Domain
```

### Architectural Rule

Dependencies ultimately point toward the Domain layer.

The Domain layer contains business concepts and has no dependencies on any other project.

The Api project references Infrastructure because it acts as the Composition Root and is responsible for wiring application services and infrastructure implementations.

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

### Infrastructure Registration

Infrastructure exposes a single:

```csharp
AddInfrastructure()
```

method that registers persistence implementations and AWS dependencies.

The Api project remains responsible only for composition and startup.

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

### Entity Creation Decision

ShortUrl instances are created through a factory method rather than public setters.

Reasons:

* Enforce invariants during creation.
* Centralize validation.
* Prevent partially initialized entities.

---

## Repository Design

### Repository Ownership

Repository contracts belong to the Application layer.

Infrastructure provides the implementation.

Current contract:

```csharp
IUrlRepository
```

### Why?

Use cases depend on abstractions rather than persistence technologies.

This allows persistence implementations to change without affecting business logic.

### Current Implementations

#### InMemoryUrlRepository

Used during early development before persistence was introduced.

#### DynamoDbUrlRepository

Production persistence implementation backed by DynamoDB.

The Create Short URL use case was migrated without changing Application or Domain code.

---

## Create Short URL Use Case

### Flow

```text
POST /shorten
    ↓
Lambda Router
    ↓
CreateShortUrlHandler
    ↓
ShortUrl Domain Entity
    ↓
IUrlRepository
    ↓
Persistence Implementation
```

### Decision

Handlers coordinate use cases but do not contain business rules.

Business logic remains inside Domain and Application layers.

---

## AWS Lambda Architecture

### Why Lambda?

* Fully managed serverless execution.
* Pay only for usage.
* Good fit for event-driven HTTP workloads.
* Minimal operational overhead.

### Key Decisions

* API Gateway HTTP API is used as the entry point.
* Lambda handlers remain thin.
* Dependency Injection is initialized during cold start.
* Business logic remains independent of Lambda-specific types.

---

## DynamoDB Integration

### Why DynamoDB?

* Serverless and fully managed.
* Low operational overhead.
* Natural fit for key-value lookups.
* Aligns with URL shortener access patterns.

### Access Patterns

Current required access patterns:

#### Create Short URL

```text
Write by ShortCode
```

#### Redirect Short URL

```text
Read by ShortCode
```

### Table Design

Table Name:

```text
UrlShortener
```

Partition Key:

```text
ShortCode (String)
```

No sort key is used.

### Partition Key Decision

ShortCode is the primary lookup value used by the application.

Example:

```http
GET /abc123
```

The system only knows the shortcode at lookup time.

Using ShortCode as the partition key enables direct item retrieval without scans or secondary indexes.

### Sort Key Decision

No current access pattern requires a sort key.

A shortcode maps to exactly one URL.

Adding a sort key would increase complexity without providing value.

### Id Decision

Id is stored as an attribute rather than a key.

Reasons:

* Application lookups are performed by ShortCode.
* No current use case retrieves records by Id.

### Billing Mode Decision

On-demand billing is used.

Reasons:

* No capacity planning required.
* Cost scales with usage.
* Appropriate for low-volume and development workloads.

---

## Configuration

### DynamoDB Configuration

The DynamoDB table name is provided through a Lambda environment variable:

```text
URL_TABLE_NAME
```

### Decision

Repositories should not directly access environment variables.

Configuration is resolved during dependency injection and passed into infrastructure components.

This keeps repositories focused on persistence concerns rather than environment-specific configuration.
