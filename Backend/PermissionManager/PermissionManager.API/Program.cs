using PermissionManager.API.Extensions;
using PermissionManager.Core.Configuration;
using PermissionManager.Core.Mapper;
using PermissionManager.Shared;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

   var configCredentials= builder.Configuration.Get<ElasticSearchAuthConfig>();
if (configCredentials == null)
{
    throw Exception("Ingresar las credenciales de elasticSearch");
}
else
{
    builder.Services.AddSingleton(configCredentials);
}

Exception Exception(string v)
{
    throw new NotImplementedException();
}

// Add services to the container.
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Configurations.ConfigureServices(builder.Services, builder.Configuration);
MapperConfiguration.ConfigurationMapper(builder.Services);

builder.Services.ConfigureElasticSearch(builder.Configuration);


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
