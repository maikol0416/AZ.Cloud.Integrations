using Domain.Port;
using MongoDB.Driver;
using Util.Common;

namespace Infrastructure
{
    public class MainContextCosmosDB : IMainContextCosmos
    {
        private IMongoDatabase Db { get; set; }

        private MongoClient MongoClient { get; set; }
        public MainContextCosmosDB(IConfigurateCosmosDB configurate)
        {
            // New instance of CosmosClient class
            MongoClient = new MongoClient(configurate.ConnectionString);
            Db = MongoClient.GetDatabase(configurate.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return !string.IsNullOrEmpty(name) ? Db.GetCollection<TEntity>(name) : null;
        }
    }
}
