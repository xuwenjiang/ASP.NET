namespace AzurePlayApi.Models.WeightTracker;

public class WeightTrackerDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string WeightsCollectionName { get; set; } = null!;
}