using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Product.Application.Products.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Add MediatR for CQRS pattern
builder.Services.AddMediatR(typeof(Program).Assembly);

// Add MediatR and scan the assembly containing your handler
builder.Services.AddMediatR(typeof(GetAllProductsHandler).Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdHandler).Assembly);
builder.Services.AddMediatR(typeof(CreateProductHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateProductHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteProductHandler).Assembly);

builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

