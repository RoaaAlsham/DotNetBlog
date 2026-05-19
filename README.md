# DotnetBlog - BackendServer


## Architecture

The solution is structured into 5 projects following Clean Architecture:

```
ZenBlogServer/
├── ZenBlog.Domain          # Entities, BaseEntity — no dependencies
├── ZenBlog.Application     # MediatR handlers, DTOs, IRepository contracts, AutoMapper profiles, FluentValidation
├── ZenBlog.Persistence     # EF Core, AppDbContext, GenericRepository, UnitOfWork, Migrations
├── ZenBlog.Infrastructure  # External services (email, storage, etc.)
└── ZenBlog.API             # Minimal API endpoints, Program.cs, middleware
```

Dependency direction: `API → Application ← Persistence`, `Application → Domain`

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | .NET 10 Web API (Minimal API) |
| ORM | Entity Framework Core |
| Database | Postgresql |
| Mediator | MediatR |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| API Docs | Scalar |
| Auth | ASP.NET Core Identity (planned) |

---

## Patterns Used

- **Clean Architecture** — strict layer separation with dependency inversion
- **Repository Pattern** — `IRepository<T>` / `GenericRepository<T>` abstraction over EF Core
- **Unit of Work** — single `SaveChangesAsync` across multiple repositories
- **CQRS with MediatR** — queries and commands are separate records, each handled by a dedicated handler
- **Result Pattern** — handlers return `Result<T>` instead of throwing exceptions for control flow
- **Pipeline Behaviours** — cross-cutting concerns (validation, logging) via `IPipelineBehavior<T>`
- **Feature-based folder structure** — each domain feature owns its Queries, Commands, Handlers, Mapping, Validators, and Results

---

## Domain Entities

- `Blog` — blog posts with title, content, image
- `Category` — blog categories
- `Comment` — comments with replies on blogs
- `ContactInfo` — site contact details
- `Message` — contact form submissions
- `SocialMedia` — social media links

---


