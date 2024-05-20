namespace KnightsChallenge.Models
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string KnightsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string KnightsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
