using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzurePlayApi.Models.WeightTracker;

public class Weight
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public decimal Value { get; set; }

    public string? Date { get; set; }
}