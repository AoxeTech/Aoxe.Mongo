using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace Zaabee.Mongo
{
    public class ZaabeeMongoClient : IZaabeeMongoClient
    {
        private MongoCollectionSettings _collectionSettings;

        private IMongoDatabase MongoDatabase { get; }

        private GuidType GuidType { get; }

        private readonly ConcurrentDictionary<Type, string> _tableNames = new ConcurrentDictionary<Type, string>();

        private readonly ConcurrentDictionary<Type, PropertyInfo> _idProperties =
            new ConcurrentDictionary<Type, PropertyInfo>();

        private MongoCollectionSettings CollectionSettings
            => _collectionSettings ?? (_collectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = GuidRepresentation.CSharpLegacy,
                ReadPreference = ReadPreference.Primary,
                WriteConcern = WriteConcern.WMajority,
                ReadConcern = ReadConcern.Default
            });

        public ZaabeeMongoClient(MongoDbConfiger configer)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            ConventionRegistry.Register("IgnoreExtraElements",
                new ConventionPack {new IgnoreExtraElementsConvention(true)}, type => true);

            GuidType = configer.GuidType;

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
            MongoDatabase = new MongoClient(settings).GetDatabase(configer.Database);
        }

        public IQueryable<T> GetQueryable<T>() where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            return MongoDatabase.GetCollection<T>(tableName, CollectionSettings).AsQueryable();
        }

        public IMongoQueryable<T> GetMongoQueryable<T>() where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            return MongoDatabase.GetCollection<T>(tableName, CollectionSettings).AsQueryable();
        }

        public void Add<T>(T entity) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertOne(entity);
        }

        public async void AddAsync<T>(T entity) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            await MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertOneAsync(entity);
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertMany(entities);
        }

        public async void AddRangeAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            await MongoDatabase.GetCollection<T>(tableName, CollectionSettings).InsertManyAsync(entities);
        }

        public long Delete<T>(T entity) where T : class, new()
        {
            if (null == entity)
                return 0;

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = collection.DeleteOne(filter);

            return result.DeletedCount;
        }

        public async Task<long> DeleteAsync<T>(T entity) where T : class, new()
        {
            if (null == entity)
                return 0;

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = await collection.DeleteOneAsync(filter);

            return result.DeletedCount;
        }

        public long Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);
            return collection.DeleteMany(where).DeletedCount;
        }

        public async Task<long> DeleteAsync<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);
            var result = await collection.DeleteManyAsync(where);
            return result.DeletedCount;
        }

        public long Update<T>(T entity) where T : class, new()
        {
            if (null == entity)
                return 0;

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = collection.UpdateOne(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        public async Task<long> UpdateAsync<T>(T entity) where T : class, new()
        {
            if (null == entity)
                return await Task.FromResult(0);

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, CollectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = await collection.UpdateOneAsync(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        private JsonFilterDefinition<T> GetJsonFilterDefinition<T>(T entity)
        {
            var propertyInfo = GetIdProperty(typeof(T));

            var isDigit = propertyInfo.PropertyType == Types.Int16 || propertyInfo.PropertyType == Types.Int32 ||
                          propertyInfo.PropertyType == Types.Int64 ||
                          propertyInfo.PropertyType == Types.UInt16 || propertyInfo.PropertyType == Types.UInt32 ||
                          propertyInfo.PropertyType == Types.UInt64;

            var value = propertyInfo.GetValue(entity, null);

            string json;
            if (isDigit)
                json = "{\"_id\":" + value + "}";
            else if (propertyInfo.PropertyType == Types.Guid)
            {
                switch (GuidType)
                {
                    case GuidType.Unspecified:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    case GuidType.Standard:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    case GuidType.CSharpLegacy:
                        json = "{\"_id\":" + $"CSUUID(\"{value}\")" + "}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
                json = "{\"_id\":\"" + value + "\"}";

            return new JsonFilterDefinition<T>(json);
        }

        private PropertyInfo GetIdProperty(Type type)
        {
            return _idProperties.GetOrAdd(type, key =>
            {
                var propertyInfo = type
                    .GetProperties()
                    .FirstOrDefault(property =>
                        Attribute.GetCustomAttributes(property).OfType<KeyAttribute>().Any());
                propertyInfo = propertyInfo ?? type.GetProperty("Id");
                propertyInfo = propertyInfo ?? type.GetProperty($"{type.Name}Id");
                if (propertyInfo == null)
                    throw new NullReferenceException("The primary key can not be found.");

                return propertyInfo;
            });
        }

        private string GetTableName(Type type)
        {
            return _tableNames.GetOrAdd(type, key =>
            {
                var attrs = Attribute.GetCustomAttributes(type);
                var tableAttr = attrs.OfType<TableAttribute>().FirstOrDefault();
                var tableName = tableAttr == null ? type.Name : tableAttr.Name;
                return tableName;
            });
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