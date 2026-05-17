using ZenBlog.Persistence.Extentions;
using Microsoft.EntityFrameworkCore;
using ZenBlog.Persistence;
using ZenBlog.Application.Extensions;
using ZenBlog.API.Endpoints.Registrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api").RegisterEndpoints();

app.Run();
