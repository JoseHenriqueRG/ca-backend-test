using BillingManagementSystem.Application;
using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using BillingManagementSystem.Infra.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepository<Product>, RepositoryProduct>();
builder.Services.AddScoped<IRepository<Customer>, RepositoryCustomer>();
builder.Services.AddScoped<IRepositoryBilling, RepositoryBilling>();
builder.Services.AddScoped<IBillingApplication, BillingApplication>();
builder.Services.AddScoped<ICrudApplication<Customer>, CustomerApplication>();
builder.Services.AddScoped<ICrudApplication<Product>, ProductApplication>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Billing Flow Manager API",
        Description = "API to manage billing flows, including handling invoices, customers and products.",
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
