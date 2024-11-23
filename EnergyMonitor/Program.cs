using EnergyMonitor.Configurations;
using EnergyMonitor.Repositories;
using EnergyMonitor.Services;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Configurações do MongoDB
var mongoConfig = builder.Configuration.GetSection("MongoConfig").Get<MongoConfig>();
if (mongoConfig == null)
{
    throw new Exception("MongoConfig section is missing or invalid in appsettings.json.");
}

var mongoClient = new MongoClient(mongoConfig.ConnectionString);
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoConfig.DatabaseName));
builder.Services.AddScoped<IConsumptionRepository, ConsumptionRepository>();

// Configurações do swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurações do Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<CacheService>();

// Adiciona o serviço de controllers
builder.Services.AddControllers();  // <- Aqui está a correção

// Adiciona controllers
var app = builder.Build();

// Habilita Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Mapeia os controllers
app.MapControllers();  // Mapear os controladores

app.Run();

