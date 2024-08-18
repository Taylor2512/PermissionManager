using PermissionManager.Consumers;
using PermissionManager.Core.Configuration;
using PermissionManager.Core.Mapper;
using PermissionManager.Producter;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddConfigureServices(builder.Configuration);
MapperConfiguration.ConfigurationMapper(builder.Services);
builder.Services.AddProducer();
builder.Services.AddHostedService<PermissionTypeJob>();
builder.Services.AddHostedService<PermissionJob>();
builder.Services.AddScoped<IPermissionConsumerService, PermissionConsumerService>();
builder.Services.AddScoped<IPermissionTypeConsumerService, PermissionTypeConsumerService>();

var host = builder.Build();
host.Run();
