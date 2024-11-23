using EnergyMonitor.Models;
using MongoDB.Driver;

namespace EnergyMonitor.Repositories;

public class ConsumptionRepository : IConsumptionRepository
{
    private readonly IMongoCollection<Consumption> _collection;

    public ConsumptionRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Consumption>("consumptions");
    }

    public async Task AddConsumption(Consumption consumption) =>
        await _collection.InsertOneAsync(consumption);

    public async Task<List<Consumption>> GetConsumptions() =>
        await _collection.Find(_ => true).ToListAsync();
}

