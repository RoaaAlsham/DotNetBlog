# ZenBlog Server

A RESTful blog API built with **ASP.NET Core (.NET 10)** following **Clean Architecture** principles. Features CQRS via MediatR, ASP.NET Core Identity, Entity Framework Core with PostgreSQL, a generic repository pattern, audit interceptors, and a FluentValidation pipeline.

---

## Table of Contents

- [Architecture Overview](#architecture-overview)
- [Project Structure](#project-structure)
- [Tech Stack](#tech-stack)
- [Domain Entities](#domain-entities)
- [Features & Endpoints](#features--endpoints)
- [Key Design Decisions](#key-design-decisions)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running Migrations](#running-migrations)

---

## Architecture Overview

ZenBlog follows **Clean Architecture** with four layers that depend strictly inward:

```
┌─────────────────────────────────────────────┐
│            Presentation                     │
│            ZenBlog.API                      │  ← Minimal API endpoints, middleware
├─────────────────────────────────────────────┤
│            Infrastructure                  │
│            ZenBlog.Persistence             │  ← EF Core, Repositories, Migrations
│            ZenBlog.Infrastructure          │  ← (reserved for external services)
├─────────────────────────────────────────────┤
│            Core                             │
│            ZenBlog.Application             │  ← CQRS, Handlers, Validators, DTOs
│            ZenBlog.Domain                  │  ← Entities, no dependencies
└─────────────────────────────────────────────┘
```

- **Domain** has zero dependencies — pure C# entities.
- **Application** depends only on Domain — no EF Core, no HTTP.
- **Persistence** implements Application contracts — EF Core and PostgreSQL live here only.
- **API** wires everything together — minimal endpoints, middleware, no business logic.

---

## Project Structure

```
ZenBlogServer/
├── .github/
│   └── workflows/
├── Core/
│   ├── ZenBlog.Application/
│   │   ├── Base/
│   │   │   ├── BaseDto.cs
│   │   │   └── BaseResult.cs
│   │   ├── Behaviors/
│   │   │   └── ValidationBehavior.cs
│   │   ├── Contracts/
│   │   │   └── Persistence/
│   │   │       ├── IRepository.cs
│   │   │       └── IUnitOfWork.cs
│   │   ├── DTOs/
│   │   │   ├── BlogDto.cs
│   │   │   ├── CategoryDto.cs
│   │   │   └── UserDto.cs
│   │   ├── Extensions/
│   │   │   └── ServiceRegistration.cs
│   │   └── Features/
│   │       ├── Blogs/
│   │       │   ├── Commands/
│   │       │   │   ├── CreateBlogCommand.cs
│   │       │   │   ├── RemoveBlogCommand.cs
│   │       │   │   └── UpdateBlogCommand.cs
│   │       │   ├── Handlers/
│   │       │   │   ├── CreateBlogCommandHandler.cs
│   │       │   │   ├── GetBlogByIdQueryHandler.cs
│   │       │   │   ├── GetBlogsByCategoryIdQueryHandler.cs
│   │       │   │   ├── GetBlogsQueryHandler.cs
│   │       │   │   ├── RemoveBlogCommandHandler.cs
│   │       │   │   └── UpdateBlogCommandHandler.cs
│   │       │   ├── Mapping/
│   │       │   │   └── BlogMappingProfile.cs
│   │       │   ├── Queries/
│   │       │   │   ├── GetBlogByIdQuery.cs
│   │       │   │   ├── GetBlogsByCategoryIdQuery.cs
│   │       │   │   └── GetBlogsQuery.cs
│   │       │   ├── Results/
│   │       │   │   ├── CreateBlogResult.cs
│   │       │   │   └── GetBlogsQueryResult.cs
│   │       │   └── Validators/
│   │       │       ├── CreateBlogValidator.cs
│   │       │       └── UpdateBlogValidator.cs
│   │       ├── Categories/
│   │       │   ├── Commands/
│   │       │   │   ├── CreateCategoryCommand.cs
│   │       │   │   ├── RemoveCategoryCommand.cs
│   │       │   │   └── UpdateCategoryCommand.cs
│   │       │   ├── Handlers/
│   │       │   │   ├── CreateCategoryCommandHandler.cs
│   │       │   │   ├── GetCategoryByIdQueryHandler.cs
│   │       │   │   ├── GetCategoryQueryHandler.cs
│   │       │   │   ├── RemoveCategoryCommandHandler.cs
│   │       │   │   └── UpdateCategoryCommandHandler.cs
│   │       │   ├── Mapping/
│   │       │   │   └── CategoryMappingProfile.cs
│   │       │   ├── Queries/
│   │       │   │   ├── GetCategoryByIdQuery.cs
│   │       │   │   └── GetCategoryQuery.cs
│   │       │   ├── Results/
│   │       │   │   └── GetCategoryQueryResult.cs
│   │       │   └── Validators/
│   │       │       ├── CreateCategoryValidator.cs
│   │       │       └── UpdateCategoryValidator.cs
│   │       ├── Comments/
│   │       │   ├── Commands/
│   │       │   │   ├── CreateCommentCommand.cs
│   │       │   │   ├── RemoveCommentCommand.cs
│   │       │   │   └── UpdateCommentCommand.cs
│   │       │   ├── Handlers/
│   │       │   │   ├── CreateCommentCommandHandler.cs
│   │       │   │   ├── DeleteCommentCommandHandler.cs
│   │       │   │   ├── GetCommentByIdQueryHandler.cs
│   │       │   │   ├── GetCommentsByBlogIdQueryHandler.cs
│   │       │   │   └── UpdateCommentCommandHandler.cs
│   │       │   ├── Mapping/
│   │       │   │   └── CommentMappingProfile.cs
│   │       │   ├── Queries/
│   │       │   │   ├── GetCommentByIdQuery.cs
│   │       │   │   └── GetCommentsByBlogIdQuery.cs
│   │       │   ├── Results/
│   │       │   │   ├── CommentResult.cs
│   │       │   │   └── CreateCommentResult.cs
│   │       │   └── Validators/
│   │       │       ├── CreateCommentCommandValidation.cs
│   │       │       └── UpdateCommentCommandValidator.cs
│   │       └── Users/
│   │           ├── Commands/
│   │           │   └── CreateUserCommand.cs
│   │           ├── Handlers/
│   │           │   ├── CreateUserCommandHandler.cs
│   │           │   └── GetAllUsersQueryHandler.cs
│   │           ├── Mappings/
│   │           │   └── UserMappingProfile.cs
│   │           ├── Queries/
│   │           │   └── GetAllUsersQuery.cs
│   │           ├── Results/
│   │           │   ├── CreateUserResult.cs
│   │           │   └── GetAllUsersQueryResult.cs
│   │           └── Validators/
│   │               └── CreateUserCommandValidator.cs
│   └── ZenBlog.Domain/
│       ├── Entities/
│       │   ├── Common/
│       │   │   └── BaseEntity.cs
│       │   ├── AppRole.cs
│       │   ├── AppUser.cs
│       │   ├── Blog.cs
│       │   ├── Category.cs
│       │   ├── Comment.cs
│       │   ├── ContactInfo.cs
│       │   ├── Message.cs
│       │   └── SocialMedia.cs
│       └── ZenBlog.Domain.csproj
├── Infrastructure/
│   ├── ZenBlog.Infrastructure/
│   │   └── ZenBlog.Infrastructure.csproj
│   └── ZenBlog.Persistence/
│       ├── Concrete/
│       │   ├── GenericRepository.cs
│       │   └── UnitOfWork.cs
│       ├── Context/
│       │   └── AppDbContext.cs
│       ├── Extentions/
│       │   └── ServiceRegistration.cs
│       ├── Intercepters/
│       │   └── AuditDbContextInterceptor.cs
│       ├── Migrations/
│       │   └── ...
│       └── ZenBlog.Persistence.csproj
└── Presentation/
    └── ZenBlog.API/
        ├── CustomMiddlewares/
        │   └── CustomExceptionHandlingMiddleware.cs
        ├── Endpoints/
        │   ├── BlogEndpoints.cs
        │   ├── CategoryEndpoints.cs
        │   ├── CommentEndpoints.cs
        │   ├── UserEndpoints.cs
        │   └── Registrations/
        │       └── EndpointRegistration.cs
        ├── Program.cs
        ├── appsettings.json
        ├── appsettings.Development.json
        └── ZenBlog.API.csproj
```

---

## Tech Stack

| Concern | Library | Version |
|---|---|---|
| Framework | ASP.NET Core Minimal APIs | .NET 10 |
| ORM | Entity Framework Core | 10.0.8 |
| Database | PostgreSQL via Npgsql | 10.0.1 |
| Identity | ASP.NET Core Identity + EF Core | 10.0.8 |
| Lazy Loading | EF Core Proxies | 10.0.8 |
| CQRS / Mediator | MediatR | — |
| Object Mapping | AutoMapper | — |
| Validation | FluentValidation | — |

---

## Domain Entities

### AppUser
Extends `IdentityUser<string>` with `FirstName`, `LastName`, and `ImageUrl`. Owns blogs and comments.

### AppRole
Extends `IdentityRole<string>` for role-based authorization.

### Blog
Core content entity. Belongs to a `Category` and an `AppUser`. Has many `Comment`s.

```csharp
public class Blog : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? BlogImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public string UserId { get; set; }
    public virtual IList<Comment> Comments { get; set; }
}
```

### Category
Groups blogs. One category has many blogs.

### Comment
Self-referencing entity supporting threaded replies.

```csharp
public class Comment : BaseEntity
{
    public string Body { get; set; }
    public Guid BlogId { get; set; }
    public string UserId { get; set; }
    public Guid? ParentCommentId { get; set; }  // null = top-level, set = reply
    public virtual IList<Comment> Replies { get; set; }
}
```

### Other Entities
`ContactInfo`, `Message`, and `SocialMedia` support site management features.

### BaseEntity
All domain entities inherit from `BaseEntity` which provides `Id` (Guid), `CreatedAt`, and `UpdatedAt` — automatically populated by `AuditDbContextInterceptor` on every save.

---

## Features & Endpoints

### Users

| Method | Route | Description |
|---|---|---|
| POST | `/users/register` | Register a new user |
| GET | `/users` | Get all users |

### Blogs

| Method | Route | Description |
|---|---|---|
| GET | `/blogs` | Get all blogs with category |
| GET | `/blogs/{id}` | Get blog by ID |
| GET | `/blogs/category/{categoryId}` | Get blogs filtered by category |
| POST | `/blogs` | Create a blog |
| PUT | `/blogs/{id}` | Update a blog |
| DELETE | `/blogs/{id}` | Remove a blog |

### Categories

| Method | Route | Description |
|---|---|---|
| GET | `/categories` | Get all categories with blogs |
| GET | `/categories/{id}` | Get category by ID |
| POST | `/categories` | Create a category |
| PUT | `/categories/{id}` | Update a category |
| DELETE | `/categories/{id}` | Remove a category |

### Comments

| Method | Route | Description |
|---|---|---|
| GET | `/comments/blog/{blogId}` | Get top-level comments for a blog |
| GET | `/comments/{id}` | Get comment with its replies |
| POST | `/comments` | Create a comment or reply |
| PUT | `/comments/{id}` | Update comment body |
| DELETE | `/comments/{id}` | Remove a comment |

---

## Key Design Decisions

### Generic Repository with Include Support
`IRepository<TEntity>` exposes `GetQuery()` for flexible querying, plus two include-capable methods that keep EF Core out of the Application layer:

```csharp
// All matching a filter with navigation properties loaded
Task<List<TEntity>> GetAllWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct,
    params Expression<Func<TEntity, object>>[] includes);

// Single entity with navigation properties loaded
Task<TEntity?> GetSingleWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct,
    params Expression<Func<TEntity, object>>[] includes);
```

### Identity Users via UserManager
`AppUser` inherits from `IdentityUser<string>` — it cannot use `IRepository<AppUser>` due to the `where TEntity : BaseEntity` constraint. All user operations go through `UserManager<AppUser>`, which is Identity's own repository abstraction.

### Flat DTOs to Prevent Circular References
Navigation properties in result DTOs use flat summary types (`CategoryDto`, `BlogDto`, `UserDto`) that never reference back to their parent — preventing infinite JSON serialization cycles:

```
Blog → GetBlogsQueryResult
         └── Category → CategoryDto  ✅ stops here, no Blogs list inside
```

### Audit Interceptor
`AuditDbContextInterceptor` automatically sets `CreatedAt` and `UpdatedAt` on every `SaveChanges` call — handlers never set these manually.

### CQRS with MediatR
Every operation is a `Command` (mutates state) or `Query` (reads state). Commands return minimal result records (`CreateBlogResult`, `CreateCommentResult`); queries return full result DTOs (`GetBlogsQueryResult`, `CommentResult`).

### Validation Pipeline
FluentValidation validators run as a MediatR `ValidationBehavior` before any handler executes:

```
mediator.Send(command)
    → ValidationBehavior  ← short-circuits with errors if invalid
        → CommandHandler  ← only runs if validation passes
```

### Global Exception Handling
`CustomExceptionHandlingMiddleware` catches all unhandled exceptions and returns a structured `BaseResult` error response — no handler needs a try/catch for unexpected errors.

### BaseResult Envelope
All responses use a consistent envelope shape:

```json
{
  "data": { "id": "...", "title": "..." },
  "errors": []
}
```

```json
{
  "data": null,
  "errors": [{ "propertyName": "Email", "errorMessage": "Email is already in use." }]
}
```

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL server (local or remote)

### Clone & Restore

```bash
git clone https://github.com/RoaaAlsham/ZenBlog.git
cd ZenBlog
dotnet restore
```

Update the connection string in `appsettings.json` (see [Configuration](#configuration)), then:

```bash
dotnet ef database update \
  --project Infrastructure/ZenBlog.Persistence \
  --startup-project Presentation/ZenBlog.API

dotnet run --project Presentation/ZenBlog.API
```

The API will be available at `https://localhost:7117`.

---

## Configuration

`Presentation/ZenBlog.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ZenBlogDb;Username=postgres;Password=yourpassword"
  }
}
```

---

## Running Migrations

```bash
# Add a new migration
dotnet ef migrations add <MigrationName> \
  --project Infrastructure/ZenBlog.Persistence \
  --startup-project Presentation/ZenBlog.API

# Apply all pending migrations
dotnet ef database update \
  --project Infrastructure/ZenBlog.Persistence \
  --startup-project Presentation/ZenBlog.API

# Revert last migration
dotnet ef migrations remove \
  --project Infrastructure/ZenBlog.Persistence \
  --startup-project Presentation/ZenBlog.API
```