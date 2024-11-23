using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnergyMonitor.Models;

public class Consumption
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("deviceName")]
    public string DeviceName { get; set; } = null!;

    [BsonElement("consumptionKwH")]
    public double ConsumptionKwH { get; set; }
}

