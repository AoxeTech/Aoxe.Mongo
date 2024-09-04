using System;
using System.Linq;
using System.Text;
using MongoDB.Driver;

public static class MongoClientSettingsExtensions
{
    public static string ToConnectionString(this MongoClientSettings settings)
    {
        var builder = new StringBuilder("mongodb://");

        // Add username and password if present
        if (
            settings.Credential != null
            && !string.IsNullOrEmpty(settings.Credential.Username)
            && !string.IsNullOrEmpty(settings.Credential.Password)
        )
        {
            builder.Append(Uri.EscapeDataString(settings.Credential.Username));
            builder.Append(":");
            builder.Append(Uri.EscapeDataString(settings.Credential.Password));
            builder.Append("@");
        }

        // Add hosts
        builder.Append(string.Join(",", settings.Servers.Select(s => s.ToString())));

        // Add database name if present
        if (settings.Credential != null && !string.IsNullOrEmpty(settings.Credential.Source))
        {
            builder.Append("/");
            builder.Append(settings.Credential.Source);
        }

        // Add options
        var options = new StringBuilder();

        if (settings.UseTls)
            options.Append("tls=true&");

        if (!string.IsNullOrEmpty(settings.ReplicaSetName))
            options.Append($"replicaSet={settings.ReplicaSetName}&");

        if (settings.Credential != null && !string.IsNullOrEmpty(settings.Credential.Source))
            options.Append($"authSource={settings.Credential.Source}&");

        if (options.Length > 0)
        {
            builder.Append("?");
            builder.Append(options.ToString().TrimEnd('&'));
        }

        return builder.ToString();
    }
}

// Example usage:
// var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://user:password@localhost:27017/mydb?authSource=admin"));
// string connectionString = settings.ToConnectionString();
// Console.WriteLine(connectionString);
