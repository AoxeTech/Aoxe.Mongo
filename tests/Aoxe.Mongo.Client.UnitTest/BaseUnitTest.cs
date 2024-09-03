namespace Aoxe.Mongo.Client.UnitTest;

public class BaseUnitTest
{
    internal static readonly IAoxeMongoClient AoxeMongoClient;

    static BaseUnitTest()
    {
        AoxeMongoClient = new AoxeMongoClient(
            new AoxeMongoOptions(
                "mongodb://admin:123@127.0.0.1:27017/admin?authSource=admin",
                "TestDB"
            )
        );
    }
}
