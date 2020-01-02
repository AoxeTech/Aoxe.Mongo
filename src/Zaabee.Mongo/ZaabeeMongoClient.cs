using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Zaabee.Mongo.Abstractions;

namespace Zaabee.Mongo
{
    public class ZaabeeMongoClient : IZaabeeMongoClient
    {
        private IMongoDatabase MongoDatabase { get; }

        private readonly ConcurrentDictionary<Type, string> _tableNames = new ConcurrentDictionary<Type, string>();

        private readonly ConcurrentDictionary<Type, PropertyInfo> _idProperties =
            new ConcurrentDictionary<Type, PropertyInfo>();

        private readonly MongoCollectionSettings _collectionSettings = new MongoCollectionSettings
        {
            AssignIdOnInsert = true,
            GuidRepresentation = GuidRepresentation.CSharpLegacy,
        };

        public ZaabeeMongoClient(string connectionString, string dataBase, DateTimeKind? dateTimeKind = null)
        {
            Init(dateTimeKind);
            MongoDatabase = new MongoClient(connectionString).GetDatabase(dataBase);
        }

        public ZaabeeMongoClient(MongoClientSettings settings, string dataBase, DateTimeKind? dateTimeKind = null)
        {
            Init(dateTimeKind);
            MongoDatabase = new MongoClient(settings).GetDatabase(dataBase);
        }

        public ZaabeeMongoClient(MongoUrl url, string dataBase, DateTimeKind? dateTimeKind = null)
        {
            Init(dateTimeKind);
            MongoDatabase = new MongoClient(url).GetDatabase(dataBase);
        }

        private static void Init(DateTimeKind? dateTimeKind = null)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            ConventionRegistry.Register("IgnoreExtraElements",
                new ConventionPack {new IgnoreExtraElementsConvention(true)}, type => true);
            if (dateTimeKind == null) return;
            var serializer = new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime);
            BsonSerializer.RegisterSerializer(typeof(DateTime), serializer);
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

            var filter = GetJsonFilterDefinition(entity, _collectionSettings.GuidRepresentation);

            var result = collection.DeleteOne(filter);

            return result.DeletedCount;
        }

        public async Task<long> DeleteAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity, _collectionSettings.GuidRepresentation);

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

            var filter = GetJsonFilterDefinition(entity, _collectionSettings.GuidRepresentation);

            var result = collection.UpdateOne(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        public async Task<long> UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);

            var filter = GetJsonFilterDefinition(entity, _collectionSettings.GuidRepresentation);

            var result = await collection.UpdateOneAsync(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument {{"$set", entity.ToBsonDocument()}}));

            return result.ModifiedCount;
        }

        public long Update<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class
        {
            if (update == null) throw new ArgumentNullException(nameof(update));
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
            if (update == null) throw new ArgumentNullException(nameof(update));
            if (where == null) throw new ArgumentNullException(nameof(where));

            var tableName = GetTableName(typeof(T));
            var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
            var updateDefinitionList = new UpdateExpressionVisitor<T>().GetUpdateDefinition(update);
            var updateDefinition = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);
            var result = await collection.UpdateManyAsync(where, updateDefinition);
            return result.ModifiedCount;
        }

        private JsonFilterDefinition<T> GetJsonFilterDefinition<T>(T entity, GuidRepresentation guidRepresentation)
        {
            var propertyInfo = GetIdProperty(typeof(T));

            var value = propertyInfo.GetValue(entity, null);

            string json;
            if (IsNumericType(propertyInfo.PropertyType))
                json = $"{{\"_id\":{value}}}";
            else if (propertyInfo.PropertyType == typeof(Guid))
            {
                switch (guidRepresentation)
                {
                    case GuidRepresentation.Unspecified:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    case GuidRepresentation.Standard:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    case GuidRepresentation.CSharpLegacy:
                        json = "{\"_id\":" + $"CSUUID(\"{value}\")" + "}";
                        break;
                    case GuidRepresentation.JavaLegacy:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    case GuidRepresentation.PythonLegacy:
                        json = "{\"_id\":" + $"UUID(\"{value}\")" + "}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
                json = $"{{\"_id\":\"{value}\"}}";

            return new JsonFilterDefinition<T>(json);
        }

        private static bool IsNumericType(Type type)
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
                var propertyInfo = type.GetProperties()
                                       .FirstOrDefault(property =>
                                           Attribute.GetCustomAttributes(property).OfType<BsonIdAttribute>().Any()) ??
                                   type.GetProperty("Id") ??
                                   type.GetProperty("id") ??
                                   type.GetProperty("_id");
                return propertyInfo ?? throw new NullReferenceException("The primary key can not be found.");
            });
        }

        private string GetTableName(Type type)
        {
            return _tableNames.GetOrAdd(type,
                key => Attribute.GetCustomAttributes(type).OfType<TableAttribute>().FirstOrDefault()?.Name ??
                       type.Name);
        }
    }
}