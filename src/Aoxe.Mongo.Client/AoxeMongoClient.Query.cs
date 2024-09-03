namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient
{
    public IQueryable<T> GetQueryable<T>()
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        return MongoDatabase.GetCollection<T>(tableName, _collectionSettings).AsQueryable();
    }
}
