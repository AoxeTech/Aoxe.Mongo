using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace Zaabee.Mongo
{
    public class ZaabeeMongoClient : IZaabeeMongoClient
    {
        private IMongoDatabase MongoDatabase { get; }

        private GuidType GuidType { get; }

        private readonly ConcurrentDictionary<Type, string> _tableNames = new ConcurrentDictionary<Type, string>();

        private readonly ConcurrentDictionary<Type, PropertyInfo> _idProperties =
            new ConcurrentDictionary<Type, PropertyInfo>();

        private readonly MongoCollectionSettings _collectionSettings = new MongoCollectionSettings
        {
            AssignIdOnInsert = true,
            GuidRepresentation = GuidRepresentation.CSharpLegacy,
        };

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

        public ZaabeeMongoClient(string connectionString, string dataBase)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            ConventionRegistry.Register("IgnoreExtraElements",
                new ConventionPack {new IgnoreExtraElementsConvention(true)}, type => true);
            MongoDatabase = new MongoClient(connectionString).GetDatabase(dataBase);
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            var tableName = GetTableName(typeof(T));
            return MongoDatabase.GetCollection<T>(tableName, _collectionSettings).AsQueryable();
        }

        public void Add<T>(T entity) where T : class
        {
            var tableName = GetTableName(typeof(T));
            MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertOne(entity);
        }

        public Task AddAsync<T>(T entity) where T : class
        {
            var tableName = GetTableName(typeof(T));
            return MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertOneAsync(entity);
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            var tableName = GetTableName(typeof(T));
            MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertMany(entities);
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            var tableName = GetTableName(typeof(T));
            return MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertManyAsync(entities);
        }

        public long Delete<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = collection.DeleteOne(filter);

            return result.DeletedCount;
        }

        public async Task<long> DeleteAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = await collection.DeleteOneAsync(filter);

            return result.DeletedCount;
        }

        public long Delete<T>(Expression<Func<T, bool>> where) where T : class
        {
            if (where == null) throw new ArgumentNullException(nameof(where));
            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
            return collection.DeleteMany(where).DeletedCount;
        }

        public async Task<long> DeleteAsync<T>(Expression<Func<T, bool>> where) where T : class
        {
            if (where == null) throw new ArgumentNullException(nameof(where));
            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
            var result = await collection.DeleteManyAsync(where);
            return result.DeletedCount;
        }

        public long Update<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = collection.UpdateOne(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        public async Task<long> UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity);

            var result = await collection.UpdateOneAsync(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        public long Update<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class
        {
            if (where == null) throw new ArgumentNullException(nameof(where));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
            var updateDefinitionList = new UpdateExpressionVisitor<T>().GetUpdateDefinition(update);
            var updateDefinition = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);
            var result = collection.UpdateMany(where, updateDefinition);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateAsync<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where)
            where T : class
        {
            if (where == null) throw new ArgumentNullException(nameof(where));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
            var updateDefinitionList = new UpdateExpressionVisitor<T>().GetUpdateDefinition(update);
            var updateDefinition = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);
            var result = await collection.UpdateManyAsync(where, updateDefinition);
            return result.ModifiedCount;
        }

        private JsonFilterDefinition<T> GetJsonFilterDefinition<T>(T entity)
        {
            var propertyInfo = GetIdProperty(typeof(T));

            var value = propertyInfo.GetValue(entity, null);

            string json;
            if (IsNumericType(propertyInfo.PropertyType))
                json = $"{{\"_id\":{value}}}";
            else if (propertyInfo.PropertyType == typeof(Guid))
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
                json = $"{{\"_id\":\"{value}\"}}";

            return new JsonFilterDefinition<T>(json);
        }

        private bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    return true;
                case TypeCode.SByte:
                    return true;
                case TypeCode.UInt16:
                    return true;
                case TypeCode.UInt32:
                    return true;
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Int16:
                    return true;
                case TypeCode.Int32:
                    return true;
                case TypeCode.Int64:
                    return true;
                case TypeCode.Decimal:
                    return true;
                case TypeCode.Double:
                    return true;
                case TypeCode.Single:
                    return true;
                case TypeCode.Boolean:
                    return false;
                case TypeCode.Char:
                    return false;
                case TypeCode.DateTime:
                    return false;
                case TypeCode.DBNull:
                    return false;
                case TypeCode.Empty:
                    return false;
                case TypeCode.Object:
                    return false;
                case TypeCode.String:
                    return false;
                default:
                    return false;
            }
        }

        private PropertyInfo GetIdProperty(Type type)
        {
            return _idProperties.GetOrAdd(type, key =>
            {
                var propertyInfo = type
                    .GetProperties()
                    .FirstOrDefault(property =>
                        Attribute.GetCustomAttributes(property).OfType<BsonIdAttribute>().Any());
                propertyInfo = propertyInfo ?? type.GetProperty("Id");
                propertyInfo = propertyInfo ?? type.GetProperty("id");
                propertyInfo = propertyInfo ?? type.GetProperty("_id");
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