namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient : IAoxeMongoClient
{
    private readonly MongoCollectionSettings _collectionSettings;
    private readonly GuidRepresentation _guidRepresentation = GuidRepresentation.CSharpLegacy;
    private readonly ConcurrentDictionary<Type, string> _tableNames = new();
    private readonly ConcurrentDictionary<Type, PropertyInfo> _idProperties = new();
    private IMongoDatabase MongoDatabase { get; }

    public AoxeMongoClient(string connectionString, string database)
        : this(new AoxeMongoOptions(connectionString, database)) { }

    public AoxeMongoClient(MongoUrl mongoUrl, string database)
        : this(new AoxeMongoOptions(mongoUrl, database)) { }

    public AoxeMongoClient(MongoClientSettings mongoClientSettings, string database)
        : this(new AoxeMongoOptions(mongoClientSettings, database)) { }

    public AoxeMongoClient(AoxeMongoOptions options)
    {
        if (
            options.MongoGuidRepresentation is not null
            && BsonSerializer.LookupSerializer<GuidSerializer>() is null
        )
        {
            var guidSerializer = new GuidSerializer(options.MongoGuidRepresentation.Value);
            BsonSerializer.RegisterSerializer(guidSerializer);
            _guidRepresentation = options.MongoGuidRepresentation.Value;
        }
        if (
            options.MongoDateTimeKind is not null
            && BsonSerializer.LookupSerializer<DateTimeSerializer>() is null
        )
        {
            var serializer = new DateTimeSerializer(
                options.MongoDateTimeKind.Value,
                BsonType.DateTime
            );
            BsonSerializer.RegisterSerializer(typeof(DateTime), serializer);
        }
        ConventionRegistry.Register(
            "IgnoreExtraElements",
            new ConventionPack { new IgnoreExtraElementsConvention(true) },
            _ => true
        );
        _collectionSettings = options.MongoCollectionSettings;
        MongoDatabase = new MongoClient(options.MongoClientSettings).GetDatabase(options.Database);
    }

    private JsonFilterDefinition<T> GetIdFilterDefinition<T>(
        T entity,
        GuidRepresentation guidRepresentation
    )
    {
        var type = typeof(T);
        var idPropertyInfo = _idProperties.GetOrAdd(
            type,
            _ =>
                type.GetProperties()
                    .FirstOrDefault(property =>
                        Attribute.GetCustomAttributes(property).OfType<KeyAttribute>().Any()
                    )
                ?? type.GetProperties()
                    .FirstOrDefault(property =>
                        Attribute.GetCustomAttributes(property).OfType<BsonIdAttribute>().Any()
                    )
                ?? type.GetProperty($"{type.Name}Id")
                ?? type.GetProperty("Id")
                ?? type.GetProperty("id")
                ?? type.GetProperty("_id")
                ?? throw new NullReferenceException("The primary key can not be found.")
        );

        var value = idPropertyInfo.GetValue(entity, null);

        string json;
        if (IsNumericType(idPropertyInfo.PropertyType))
            json = $"{{\"_id\":{value}}}";
        else if (idPropertyInfo.PropertyType == typeof(Guid))
        {
            json = guidRepresentation switch
            {
                GuidRepresentation.Unspecified => "{\"_id\":" + $"UUID(\"{value}\")" + "}",
                GuidRepresentation.Standard => "{\"_id\":" + $"UUID(\"{value}\")" + "}",
                GuidRepresentation.CSharpLegacy => "{\"_id\":" + $"CSUUID(\"{value}\")" + "}",
                GuidRepresentation.JavaLegacy => "{\"_id\":" + $"UUID(\"{value}\")" + "}",
                GuidRepresentation.PythonLegacy => "{\"_id\":" + $"UUID(\"{value}\")" + "}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        else
            json = $"{{\"_id\":\"{value}\"}}";

        return new JsonFilterDefinition<T>(json);
    }

    private static bool IsNumericType(Type type) =>
        Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => true,
            TypeCode.SByte => true,
            TypeCode.UInt16 => true,
            TypeCode.UInt32 => true,
            TypeCode.UInt64 => true,
            TypeCode.Int16 => true,
            TypeCode.Int32 => true,
            TypeCode.Int64 => true,
            TypeCode.Decimal => true,
            TypeCode.Double => true,
            TypeCode.Single => true,
            _ => false
        };

    private string GetTableName(Type type) =>
        _tableNames.GetOrAdd(
            type,
            _ =>
                Attribute.GetCustomAttributes(type).OfType<TableAttribute>().FirstOrDefault()?.Name
                ?? type.Name
        );
}
