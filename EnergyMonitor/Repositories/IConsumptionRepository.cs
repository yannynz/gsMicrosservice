using EnergyMonitor.Models;

namespace EnergyMonitor.Repositories;

public interface IConsumptionRepository
{
    Task AddConsumption(Consumption consumption);
    Task<List<Consumption>> GetConsumptions();
}

