using MongoDB.Driver;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;

namespace Zaabee.Mongo.UnitTest
{
    public class BaseUnitTest
    {
        internal static readonly IZaabeeMongoClient ZaabeeMongoClient;

        static BaseUnitTest()
        {
            var client = new MongoClient(
                "mongodb://admin:123@192.168.78.140:27017,192.168.78.141:27017,192.168.78.142/admin?authSource=admin&replicaSet=rs");
            ZaabeeMongoClient = new ZaabeeMongoClient(client, "TestDB");
        }
    }
}