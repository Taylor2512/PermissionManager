using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PermissionManager.Consumer;
using PermissionManager.Core.Configuration;
using PermissionManager.Core.Mapper;

Console.WriteLine("Start consuming events ...");

var builder = Host.CreateApplicationBuilder();

builder.Services.AddConfigureServices(builder.Configuration);
MapperConfiguration.ConfigurationMapper(builder.Services);
builder.Services.AddHostedService<ConsumerService>();

builder.Build().Run();