# ZenBlog Server

A RESTful blog API built with **ASP.NET Core** following **Clean Architecture** principles. Features CQRS via MediatR, Identity-based authentication, Entity Framework Core, and a generic repository pattern.

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
┌─────────────────────────────────────────┐
│              ZenBlog.API                │  ← Endpoints, HTTP concerns
├─────────────────────────────────────────┤
│          ZenBlog.Application            │  ← CQRS, Handlers, Validators, DTOs
├─────────────────────────────────────────┤
│           ZenBlog.Domain                │  ← Entities, BaseEntity
├─────────────────────────────────────────┤
│         ZenBlog.Persistence             │  ← EF Core, Repositories, Migrations
└─────────────────────────────────────────┘
```

- **Domain** has zero dependencies — pure C# entities.
- **Application** depends only on Domain — no EF Core, no HTTP.
- **Persistence** implements Application contracts — EF Core lives here only.
- **API** wires everything together — minimal endpoints, no business logic.

---

## Project Structure

```
ZenBlogServer/
├── ZenBlog.API/
│   └── Endpoints/
│       ├── BlogEndpoints.cs
│       ├── CategoryEndpoints.cs
│       ├── CommentEndpoints.cs
│       └── UserEndpoints.cs
│
├── ZenBlog.Application/
│   ├── Base/
│   │   ├── BaseDto.cs
│   │   └── BaseResult.cs
│   ├── Behaviors/
│   │   └── ValidationBehavior.cs
│   ├── Contracts/
│   │   └── Persistence/
│   │       ├── IRepository.cs
│   │       └── IUnitOfWork.cs
│   ├── DTOs/
│   │   ├── BlogDto.cs
│   │   ├── CategoryDto.cs
│   │   └── UserDto.cs
│   └── Features/
│       ├── Blogs/
│       │   ├── Commands/
│       │   ├── Handlers/
│       │   ├── Mapping/
│       │   ├── Queries/
│       │   ├── Results/
│       │   └── Validators/
│       ├── Categories/
│       ├── Comments/
│       └── Users/
│
├── ZenBlog.Domain/
│   └── Entities/
│       ├── Common/
│       │   └── BaseEntity.cs
│       ├── AppUser.cs
│       ├── AppRole.cs
│       ├── Blog.cs
│       ├── Category.cs
│       └── Comment.cs
│
└── ZenBlog.Persistence/
    ├── Context/
    │   └── AppDbContext.cs
    └── Concrete/
        └── GenericRepository.cs
```

---

## Tech Stack

| Concern | Library |
|---|---|
| Framework | ASP.NET Core (Minimal APIs) |
| ORM | Entity Framework Core |
| CQRS / Mediator | MediatR |
| Object Mapping | AutoMapper |
| Validation | FluentValidation |
| Authentication | ASP.NET Core Identity |
| Database | SQL Server |

---

## Domain Entities

### AppUser
Extends `IdentityUser<string>` with `FirstName`, `LastName`, and `ImageUrl`. Owns blogs and comments.

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
}
```

### Category
Groups blogs. One category has many blogs.

### Comment
Self-referencing entity supporting nested replies.

```csharp
public class Comment : BaseEntity
{
    public string Body { get; set; }
    public Guid BlogId { get; set; }
    public string UserId { get; set; }
    public Guid? ParentCommentId { get; set; }  // null = top-level, set = reply
    public IList<Comment> Replies { get; set; }
}
```

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
| GET | `/blogs/category/{categoryId}` | Get blogs by category |
| POST | `/blogs` | Create a blog |
| PUT | `/blogs/{id}` | Update a blog |

### Categories

| Method | Route | Description |
|---|---|---|
| GET | `/categories` | Get all categories with blogs |
| GET | `/categories/{id}` | Get category by ID |
| POST | `/categories` | Create a category |

### Comments

| Method | Route | Description |
|---|---|---|
| GET | `/comments/blog/{blogId}` | Get top-level comments for a blog |
| GET | `/comments/{id}` | Get comment with replies |
| POST | `/comments` | Create comment or reply |
| PUT | `/comments/{id}` | Update comment body |
| DELETE | `/comments/{id}` | Delete comment |

---

## Key Design Decisions

### Generic Repository with Include Support
`IRepository<TEntity>` exposes `GetQuery()` for flexible querying and two include-capable methods to keep EF Core out of the Application layer:

```csharp
// Fetch all matching a filter with navigation properties
Task<List<TEntity>> GetAllWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct,
    params Expression<Func<TEntity, object>>[] includes);

// Fetch single with navigation properties
Task<TEntity?> GetSingleWithIncludesAsync(
    Expression<Func<TEntity, bool>> filter,
    CancellationToken ct,
    params Expression<Func<TEntity, object>>[] includes);
```

### Identity Users via UserManager
`AppUser` inherits from `IdentityUser<string>` — it cannot use `IRepository<AppUser>` due to the `where TEntity : BaseEntity` constraint. All user operations go through `UserManager<AppUser>`.

### Flat DTOs to Prevent Circular References
Navigation properties in result DTOs use flat summary types (`CategoryDto`, `BlogDto`, `UserDto`) that never reference back to their parent — preventing infinite JSON serialization cycles.

```
Blog → BlogResult
         └── Category → CategoryDto  ✅ stops here, no Blogs list inside
```

### CQRS with MediatR
Every operation is a `Command` (mutates state) or `Query` (reads state). Commands return minimal result records; queries return full result DTOs.

### Validation Pipeline
FluentValidation validators are automatically discovered and executed as a MediatR pipeline behavior before any handler runs:

```
mediator.Send(command)
    → ValidationBehavior  ← runs validator, throws if invalid
        → CommandHandler  ← only runs if valid
```

### BaseResult<T> Envelope
All responses are wrapped in a consistent envelope:

```json
{
  "data": { ... },
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
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or update connection string for SQLite/PostgreSQL)

### Clone & Run

```bash
git clone https://github.com/RoaaAlsham/DotNetBlog.git
cd DotNetBlog
dotnet restore
```

Update the connection string in `appsettings.json` (see [Configuration](#configuration)), then:

```bash
dotnet ef database update --project ZenBlog.Persistence --startup-project ZenBlog.API
dotnet run --project ZenBlog.API
```

The API will be available at `https://localhost:7117`.

---

## Configuration

`ZenBlog.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ZenBlogDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "ZenBlog",
    "Audience": "ZenBlog",
    "ExpiryMinutes": 60
  }
}
```

---

## Running Migrations

```bash
# Add a new migration
dotnet ef migrations add <MigrationName> \
  --project ZenBlog.Persistence \
  --startup-project ZenBlog.API

# Apply migrations
dotnet ef database update \
  --project ZenBlog.Persistence \
  --startup-project ZenBlog.API

# Revert last migration
dotnet ef migrations remove \
  --project ZenBlog.Persistence \
  --startup-project ZenBlog.API
```