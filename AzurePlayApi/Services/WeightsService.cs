using AzurePlayApi.Models.WeightTracker;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AzurePlayApi.Services;

public class WeightsService
{
    private readonly IMongoCollection<Weight> _weightsCollection;

    public WeightsService(
        IOptions<WeightTrackerDatabaseSettings> weightTrackerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            weightTrackerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            weightTrackerDatabaseSettings.Value.DatabaseName);

        _weightsCollection = mongoDatabase.GetCollection<Weight>(
            weightTrackerDatabaseSettings.Value.WeightsCollectionName);
    }

    public async Task<List<Weight>> GetAsync() =>
        await _weightsCollection.Find(_ => true).ToListAsync();

    public async Task<Weight?> GetAsync(string id) =>
        await _weightsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Weight newWeight) =>
        await _weightsCollection.InsertOneAsync(newWeight);

    public async Task UpdateAsync(string id, Weight updatedWeight) =>
        await _weightsCollection.ReplaceOneAsync(x => x.Id == id, updatedWeight);

    public async Task RemoveAsync(string id) =>
        await _weightsCollection.DeleteOneAsync(x => x.Id == id);
}