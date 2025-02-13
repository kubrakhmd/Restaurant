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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
 
builder .Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo { Title = "QR API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });


});
BarcodeSettings settings = new BarcodeSettings();
settings.Type = BarCodeType.QRCode;
settings.QRCodeECL = QRCodeECL.M;
settings.ShowText = false;
settings.X = 2.5f;
string data = "https://twitter.com";
settings.Data = data;
settings.Data2D = data;

//Add an image to QR code
settings.QRCodeLogoImage = Image.FromFile(@"C:\Users\Kubra\OneDrive\Documents\Pictures");

//Instantiate a BarCodeGenerator object
BarCodeGenerator generator = new BarCodeGenerator(settings);
//Generate QR image
Image image = generator.GenerateImage();
//Save the image
image.Save("QR.png", System.Drawing.Imaging.ImageFormat.Png);            
        
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
    
