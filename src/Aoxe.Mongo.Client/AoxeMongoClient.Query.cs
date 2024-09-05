namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient
{
    public IQueryable<T> GetQueryable<T>()
        where T : class =>
        MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), _collectionSettings).AsQueryable();
}
