using Xunit;
using EnergyMonitor.Models;

namespace EnergyMonitor.Tests;

public class ConsumptionTests
{
    [Fact]
    public void CanCreateConsumption()
    {
        var consumption = new Consumption
        {
            DeviceName = "Lamp",
            ConsumptionKwH = 5.5
        };

        Assert.NotNull(consumption);
        Assert.Equal("Lamp", consumption.DeviceName);
        Assert.Equal(5.5, consumption.ConsumptionKwH);
    }
}

