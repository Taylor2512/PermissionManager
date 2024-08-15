using PermissionManager.API.Extensions;
using PermissionManager.Core.Mapper;

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Configurations.ConfigureServices(builder.Services, builder.Configuration);
MapperConfiguration.ConfigurationMapper(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
}); // Add this line to allow all origins

app.UseAuthorization();


app.MapControllers();

app.Run();
