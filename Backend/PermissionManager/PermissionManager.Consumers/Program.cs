using PermissionManager.Consumers;
using PermissionManager.Core.Configuration;
using PermissionManager.Core.Mapper;
using PermissionManager.Producter;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddConfigureServices(builder.Configuration);
MapperConfiguration.ConfigurationMapper(builder.Services);
builder.Services.AddProducer();
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IKafkaConsumerService, KafkaConsumerService>();

var host = builder.Build();
host.Run();
