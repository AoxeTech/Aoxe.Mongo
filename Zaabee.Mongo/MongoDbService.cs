using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Zaabee.Mongo.Common;

namespace Zaabee.Mongo
{
    public class MongoDbService
    {
        private MongoCollectionSettings _collectionSettings;

        private IMongoDatabase MongoDatabase { get; }

        private IMongoClient MongoClient { get; }

        private MongoCollectionSettings CollectionSettings
            => _collectionSettings ?? (_collectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = GuidRepresentation.CSharpLegacy,
                ReadPreference = ReadPreference.Primary,
                WriteConcern = WriteConcern.WMajority,
                ReadConcern = ReadConcern.Default
            });

        public MongoDbService(MongoDbConfiger configer)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            ConventionRegistry.Register("IgnoreExtraElements",
                new ConventionPack {new IgnoreExtraElementsConvention(true)}, type => true);
            
            var settings = new MongoClientSettings
            {
                Servers = configer.Hosts.Select(p =>
                {
                    var strs = p.Split(':');
                    var ip = strs[0];
                    var port = string.IsNullOrWhiteSpace(strs[1]) ? 27017 : Convert.ToInt32(strs[1]);
                    return new MongoServerAddress(ip, port);
                }),
                ReadPreference = configer.ReadPreference != null
                    ? new ReadPreference(ConvertReadPreference(configer.ReadPreference.Value))
                    : new ReadPreference(ReadPreferenceMode.Primary),
                Credential =
                    MongoCredential.CreateCredential(configer.Database, configer.UserName, configer.Password),
                ConnectTimeout = TimeSpan.FromSeconds(300),
                SocketTimeout = TimeSpan.FromSeconds(300),
                MaxConnectionLifeTime = TimeSpan.FromSeconds(300),
                MaxConnectionPoolSize = configer.MaxConnectionPoolSize,
                MinConnectionPoolSize = configer.MinConnectionPoolSize,
                ConnectionMode = ConnectionMode.Automatic
            };
            MongoClient = new MongoClient(settings);
            MongoDatabase = MongoClient.GetDatabase(configer.Database);
        }

        public IQueryable<T> GetQueryable<T>(string tableName) where T : class, new()
        {
            return MongoDatabase.GetCollection<T>(tableName, CollectionSettings).AsQueryable();
        }

        public void Add<T>(T entity, string tableName) where T : class
        {
            MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertOne(entity);
        }

        public void AddRange<T>(IEnumerable<T> entities, string tableName) where T : class, new()
        {
            MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertMany(entities);
        }
        
        public long Delete<T>(Expression<Func<T, bool>> where, string tableName) where T : class, new()
        {
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);
            return collection.DeleteMany(where).DeletedCount;
        }
        
        public long Update<T>(T entity, string tableName)
        {
            if (null == entity)
                return 0;

            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);

            var filter = GetJsonFilterDefintion(entity);

            var result = collection.UpdateOne(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }
        
        private static JsonFilterDefinition<T> GetJsonFilterDefintion<T>(T entity)
        {
            var propertyInfo = entity.GetType().GetProperty("Id");
            if (propertyInfo == null) throw new NullReferenceException("主键名必须为Id。");

            var value = propertyInfo.GetValue(entity, null);

            string json;
            var isDigit = propertyInfo.PropertyType == Types.Int32 || propertyInfo.PropertyType == Types.Int64 ||
                          propertyInfo.PropertyType == Types.Int16 || propertyInfo.PropertyType == Types.UInt32;

            if (isDigit)
                json = "{\"_id\":" + value + "}";
            else
                json = "{\"_id\":\"" + value + "\"}";

            return new JsonFilterDefinition<T>(json);
        }

        private ReadPreferenceMode ConvertReadPreference(MongoDbReadPreference readPreference)
        {
            switch (readPreference)
            {
                case MongoDbReadPreference.Primary:
                    return ReadPreferenceMode.Primary;
                case MongoDbReadPreference.PrimaryPreferred:
                    return ReadPreferenceMode.PrimaryPreferred;
                case MongoDbReadPreference.Secondary:
                    return ReadPreferenceMode.Secondary;
                case MongoDbReadPreference.SecondaryPreferred:
                    return ReadPreferenceMode.SecondaryPreferred;
                case MongoDbReadPreference.Nearest:
                    return ReadPreferenceMode.Nearest;
                default:
                    throw new ArgumentOutOfRangeException(nameof(readPreference), readPreference, null);
            }
        }
    }
}