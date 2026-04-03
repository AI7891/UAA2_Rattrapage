using ApplicationCoreBusiness.Interfaces.IRepositories;
using ApplicationCoreBusiness.Interfaces.IServices;
using ApplicationCoreBusiness.Services;
using DomainEntityModels.Enums;
using InfrastructureDatabase;
using InfrastructureDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    npgsqlOptions => 
    { 
        npgsqlOptions.MapEnum<MemberStatus>("MemberStatus"); 
    })
);
// Register the MemberRepository as the implementation for the IMember_Repository interface, allowing for dependency injection of the repository in the application. This enables the application to use the repository for data access operations related to members.
builder.Services.AddScoped<IMember_Repository, MemberRepository>();

// Register the MemberService as the implementation for the IMemberService interface, allowing for dependency injection of the service in the application. This enables the application to use the service for business logic operations related to members, such as creating, deleting, and retrieving members.
builder.Services.AddScoped<IMemberService, MemberService>();




builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
