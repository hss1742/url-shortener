# URL Shortener

A production-style URL Shortener built using .NET 8 and AWS serverless services.

## Tech Stack

* .NET 8
* AWS Lambda
* API Gateway (HTTP API)
* GitHub Actions
* Clean Architecture
* DynamoDB (planned)

---

## Solution Structure

```text
src/
├── UrlShortener.Api
├── UrlShortener.Application
├── UrlShortener.Domain
└── UrlShortener.Infrastructure
```

### Layer Responsibilities

#### UrlShortener.Api

Responsible for:

* AWS Lambda handlers
* API Gateway integration
* Request/response translation

#### UrlShortener.Application

Responsible for:

* Use cases
* Application services
* Contracts and abstractions

Current implementation:

```text
Health
├── HealthResponse
└── HealthService
```

#### UrlShortener.Domain

Responsible for:

* Business entities
* Value objects
* Business rules

Currently empty until URL-shortening domain concepts are introduced.

#### UrlShortener.Infrastructure

Responsible for:

* External integrations
* Persistence
* AWS service implementations

Currently empty until DynamoDB integration is introduced.

---

## Architecture

```text
Client
  ↓
API Gateway
  ↓
AWS Lambda (Api Layer)
  ↓
Application Layer
  ↓
Domain Layer

Infrastructure Layer
        ↓
Application Layer
        ↓
Domain Layer
```

### Dependency Direction

```text
Api
 ↓
Application
 ↓
Domain

Infrastructure
 ↓
Application
 ↓
Domain
```

Domain has no dependencies.

---

## Health Endpoint

```http
GET /health
```

Example response:

```json
{
  "message": "URL Shortener API deployed from GitHub Actions"
}
```

---

## CI/CD Architecture

```text
Developer
    ↓
git push
    ↓
GitHub Actions
    ↓
OIDC Authentication
    ↓
AWS IAM Role
    ↓
AWS Lambda Deployment
    ↓
API Gateway
    ↓
Live Endpoint Updated
```

### Authentication

GitHub Actions authenticates to AWS using OpenID Connect (OIDC).

No long-lived AWS credentials are stored in GitHub.

Temporary AWS credentials are issued by AWS STS during deployment.

---

## Infrastructure Status

### Completed

* AWS Lambda Function
* API Gateway HTTP API
* Lambda ↔ API Gateway Integration
* Clean Architecture Foundation
* GitHub Actions CI Pipeline
* GitHub Actions CD Pipeline
* OIDC Authentication
* IAM Deployment Role
* Automated Lambda Deployment
* Public Health Endpoint

### Planned

* Dependency Injection
* Repository Abstractions
* URL Creation Endpoint
* URL Redirection Endpoint
* Short Code Generation
* DynamoDB Integration
* Analytics Pipeline
* Monitoring & Observability

---

## Deployment

Deployments are fully automated.

```text
Code Change
    ↓
git push
    ↓
GitHub Actions
    ↓
Build
    ↓
Deploy Lambda
    ↓
Production Updated
```

Manual deployment remains available:

```bash
cd src/UrlShortener.Api
dotnet lambda deploy-function url-shortener-api
```
