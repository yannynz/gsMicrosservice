using EnergyMonitor.Models;
using EnergyMonitor.Repositories;
using EnergyMonitor.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsumptionController : ControllerBase
{
    private readonly IConsumptionRepository _repository;
    private readonly CacheService _cacheService;

    public ConsumptionController(IConsumptionRepository repository, CacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }

    [HttpGet]
    public async Task<IActionResult> GetConsumptions()
    {
        const string cacheKey = "consumptions";
        var cachedData = await _cacheService.GetCacheAsync<List<Consumption>>(cacheKey);

        if (cachedData != null)
            return Ok(cachedData);

        var data = await _repository.GetConsumptions();
        await _cacheService.SetCacheAsync(cacheKey, data, TimeSpan.FromMinutes(5));

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> AddConsumption([FromBody] Consumption consumption)
    {
        await _repository.AddConsumption(consumption);
        return CreatedAtAction(nameof(AddConsumption), consumption);
    }
}

