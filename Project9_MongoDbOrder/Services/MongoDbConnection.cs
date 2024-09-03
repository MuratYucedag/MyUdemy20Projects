using MongoDB.Bson;
using MongoDB.Driver;

namespace Project9_MongoDbOrder.Services
{
    public class MongoDbConnection
    {
        private IMongoDatabase _database;

        public MongoDbConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Db9ProjectOrder");
        }

        public IMongoCollection<BsonDocument> GetOrdersCollection()
        {
            return _database.GetCollection<BsonDocument>("Orders");
        }
    }
}
