using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NuGet.Configuration;
using QRCoder;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Implementations.Repositories;
using Restaurant.Persistence.Implementations.Services;
using Spire.Barcode;
using Restaurant.Persistence.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(builder.Configuration) ;

var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

    app.Run();
    
