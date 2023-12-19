using MongoDB.Driver;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class BaseUnitTest
    {
        internal static readonly IMongoDatabase MongoDatabase;

        static BaseUnitTest()
        {
            var client = new MongoClient(
                "mongodb://admin:123@192.168.78.140:27017,192.168.78.141:27017,192.168.78.142/admin?authSource=admin&replicaSet=rs"
            );
            MongoDatabase = client.GetDatabase("TestDB");
        }
    }
}
