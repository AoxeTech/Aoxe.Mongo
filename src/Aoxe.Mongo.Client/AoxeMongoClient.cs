namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient : IAoxeMongoClient
{
    private readonly MongoCollectionSettings _collectionSettings;
    private readonly GuidSerializer _guidSerializer;
    private readonly ConcurrentDictionary<Type, string> _tableNames = new();
    private readonly ConcurrentDictionary<Type, PropertyInfo> _idProperties = new();

    private IMongoDatabase MongoDatabase { get; }

    public AoxeMongoClient(AoxeMongoOptions options)
    {
        var guidSerializer = new GuidSerializer(options.GuidRepresentation);
        BsonSerializer.RegisterSerializer(guidSerializer);
        _guidSerializer = guidSerializer;
        ConventionRegistry.Register(
            "IgnoreExtraElements",
            new ConventionPack { new IgnoreExtraElementsConvention(true) },
            _ => true
        );
        var serializer = new DateTimeSerializer(options.DateTimeKind, BsonType.DateTime);
        BsonSerializer.RegisterSerializer(typeof(DateTime), serializer);
        _collectionSettings = options.MongoCollectionSettings;
        MongoDatabase = new MongoClient(options.MongoClientSettings).GetDatabase(options.Database);
    }

    private JsonFilterDefinition<T> GetJsonFilterDefinition<T>(
        T entity,
        GuidSerializer guidSerializer
    )
    {
        var idPropertyInfo = GetIdProperty(typeof(T));

        var value = idPropertyInfo.GetValue(entity, null);

        string json;
        if (IsNumericType(idPropertyInfo.PropertyType))
            json = $"{{\"_id\":{value}}}";
        else if (idPropertyInfo.PropertyType == typeof(Guid))
        {
            json = guidSerializer.GuidRepresentation switch
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

    private PropertyInfo GetIdProperty(Type type) =>
        _idProperties.GetOrAdd(
            type,
            _ =>
            {
                var propertyInfo =
                    type.GetProperties()
                        .FirstOrDefault(
                            property =>
                                Attribute
                                    .GetCustomAttributes(property)
                                    .OfType<BsonIdAttribute>()
                                    .Any()
                        )
                    ?? type.GetProperty("Id")
                    ?? type.GetProperty("id")
                    ?? type.GetProperty("_id");
                return propertyInfo
                    ?? throw new NullReferenceException("The primary key can not be found.");
            }
        );

    private string GetTableName(Type type) =>
        _tableNames.GetOrAdd(
            type,
            _ =>
                Attribute.GetCustomAttributes(type).OfType<TableAttribute>().FirstOrDefault()?.Name
                ?? type.Name
        );
}
