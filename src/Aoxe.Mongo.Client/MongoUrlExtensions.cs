using MongoDB.Driver.Core.Configuration;

namespace Aoxe.Mongo.Client;

public static class MongoUrlExtensions
{
    public static string ToConnectionString(this MongoUrl mongoUrl)
    {
        var builder = new UriBuilder
        {
            Scheme = GetSchemeString(mongoUrl.Scheme),
            Host = mongoUrl.Server.Host,
            Port = mongoUrl.Server.Port
        };

        // Add username and password if present
        if (!string.IsNullOrEmpty(mongoUrl.Username) && !string.IsNullOrEmpty(mongoUrl.Password))
        {
            builder.UserName = Uri.EscapeDataString(mongoUrl.Username);
            builder.Password = Uri.EscapeDataString(mongoUrl.Password);
        }

        // Add database name if present
        if (!string.IsNullOrEmpty(mongoUrl.DatabaseName))
        {
            builder.Path = "/" + mongoUrl.DatabaseName;
        }

        // Add query parameters
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(mongoUrl.AuthenticationSource))
            queryParams.Add($"authSource={Uri.EscapeDataString(mongoUrl.AuthenticationSource)}");

        if (!string.IsNullOrEmpty(mongoUrl.AuthenticationMechanism))
            queryParams.Add(
                $"authMechanism={Uri.EscapeDataString(mongoUrl.AuthenticationMechanism)}"
            );

        if (mongoUrl.UseTls)
            queryParams.Add("tls=true");

        if (!string.IsNullOrEmpty(mongoUrl.ReplicaSetName))
            queryParams.Add($"replicaSet={Uri.EscapeDataString(mongoUrl.ReplicaSetName)}");

        // Add any other parameters you need here

        if (queryParams.Count > 0)
        {
            builder.Query = string.Join("&", queryParams);
        }

        return builder.ToString();
    }

    private static string GetSchemeString(ConnectionStringScheme scheme) =>
        scheme switch
        {
            ConnectionStringScheme.MongoDB => "mongodb",
            ConnectionStringScheme.MongoDBPlusSrv => "mongodb+srv",
            _ => throw new ArgumentException($"Unsupported scheme: {scheme}")
        };
}
