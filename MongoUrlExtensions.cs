using System;
using System.Linq;
using MongoDB.Driver;

public static class MongoUrlExtensions
{
    public static string ToConnectionString(this MongoUrl mongoUrl)
    {
        var builder = new UriBuilder
        {
            Scheme = mongoUrl.Scheme,
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
        var query = string.Join(
            "&",
            mongoUrl.QueryString.AllKeys.Select(key =>
                $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(mongoUrl.QueryString[key])}"
            )
        );

        if (!string.IsNullOrEmpty(query))
        {
            builder.Query = query;
        }

        return builder.ToString();
    }
}

// Example usage:
// var mongoUrl = new MongoUrl("mongodb://user:password@localhost:27017/mydb?authSource=admin");
// string connectionString = mongoUrl.ToConnectionString();
// Console.WriteLine(connectionString);
