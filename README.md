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

## Project Structure

```text
url-shortener/
│
├── .github/
│   └── workflows/
│       ├── ci.yml
│       └── deploy.yml
│
├── src/
│   └── UrlShortener.Api/
│
├── UrlShortener.sln
├── README.md
└── .gitignore
```

---

## Development Setup

### Prerequisites

* .NET 8 SDK
* AWS CLI
* Amazon Lambda Tools

Install Lambda Tools:

```bash
dotnet tool install -g Amazon.Lambda.Tools
```

Update Lambda Tools:

```bash
dotnet tool update -g Amazon.Lambda.Tools
```

---

## Build

From the repository root:

```bash
dotnet restore
dotnet build
```

---

## Run Tests

```bash
dotnet test
```

---

## Deploy Lambda

Deploy the function to AWS:

```bash
cd src/UrlShortener.Api
dotnet lambda deploy-function url-shortener-api
```

---

## Infrastructure Status

### Completed

* AWS Lambda Function
* API Gateway HTTP API
* Lambda ↔ API Gateway Integration
* CI Pipeline (GitHub Actions)
* Public Health Endpoint

### Planned

* Automated CD Pipeline
* Clean Architecture Layers
* URL Creation Endpoint
* Short Code Generation
* DynamoDB Integration
* Analytics Pipeline

---

## Learning Goals

This project is being built incrementally to develop:

* Backend Engineering Skills
* AWS Serverless Architecture Skills
* CI/CD Experience
* Production System Design Skills
* SDE2-Level Engineering Practices

```
```
