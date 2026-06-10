# URL Shortener

A production-style URL Shortener built using .NET 8 and AWS serverless services.

## Tech Stack

* .NET 8
* AWS Lambda
* API Gateway (HTTP API)
* DynamoDB (planned)
* GitHub Actions
* Clean Architecture (planned)

---

## Current Architecture

```text
Client
  ↓
API Gateway (HTTP API)
  ↓
AWS Lambda (.NET 8)
  ↓
Response

Future:
  ↓
DynamoDB
```

### Health Endpoint

```http
GET /health
```

Response:

```text
URL Shortener API is healthy
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

No long-lived AWS access keys are stored in GitHub Secrets.

Temporary AWS credentials are issued by AWS STS during deployment.

---

## Infrastructure Status

### Completed

* AWS Lambda Function
* API Gateway HTTP API
* Lambda ↔ API Gateway Integration
* GitHub Actions CI Pipeline
* GitHub Actions CD Pipeline
* OIDC Authentication
* IAM Deployment Role
* Automated Lambda Deployment
* Public Health Endpoint

### Planned

* Clean Architecture Layers
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
