using Aoxe.Mongo.Abstractions;

namespace Aoxe.Mongo.UnitTest
{
    public class BaseUnitTest
    {
        internal static readonly IAoxeMongoClient AoxeMongoClient;

        static BaseUnitTest()
        {
            AoxeMongoClient = new AoxeMongoClient(
                new AoxeMongoOptions
                {
                    ConnectionString =
                        "mongodb://admin:123@192.168.78.140:27017,192.168.78.141:27017,192.168.78.142/admin?authSource=admin&replicaSet=rs",
                    Database = "TestDB"
                }
            );
        }
    }
}
