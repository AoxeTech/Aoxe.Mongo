using System;
using System.Collections.Generic;

namespace Zaabee.Mongo.Common
{
    public class MongoDbConfiger
    {
        public MongoDbConfiger(List<string> hosts, string database,string authSource, string userName, string password,
            MongoDbReadPreference? readPreference = null, int maxConnectionPoolSize = 100,
            int minConnectionPoolSize = 0, GuidType guidType = GuidType.CSharpLegacy)
        {
            Hosts = hosts;
            Database = database;
            AuthSource = authSource;
            UserName = userName;
            Password = password;
            ReadPreference = readPreference;
            MaxConnectionPoolSize = maxConnectionPoolSize;
            MinConnectionPoolSize = minConnectionPoolSize;
            GuidType = guidType;
        }

        public MongoDbConfiger()
        {
        }

        private List<string> _hosts;

        public List<string> Hosts
        {
            get => _hosts ?? (_hosts = new List<string>());
            private set => _hosts = value;
        }

        public string Database { get; set; }

        private string _authSource;

        public string AuthSource
        {
            get => string.IsNullOrWhiteSpace(_authSource) ? Database : _authSource;
            set => _authSource = value;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int MaxConnectionPoolSize { get; set; }

        public int MinConnectionPoolSize { get; set; }

        public MongoDbReadPreference? ReadPreference { get; set; }

        public GuidType GuidType { get; set; }

        public string GetConnectionString()
        {
            return $"mongodb://{UserName}:{Password}@{string.Join(",", Hosts)}/{Database}{GetReadPreferenceStr()}";
        }

        private string GetReadPreferenceStr()
        {
            switch (ReadPreference)
            {
                case MongoDbReadPreference.Primary:
                    return "/?readPreference=primary";
                case MongoDbReadPreference.PrimaryPreferred:
                    return "/?readPreference=primaryPreferred";
                case MongoDbReadPreference.Secondary:
                    return "/?readPreference=secondary";
                case MongoDbReadPreference.SecondaryPreferred:
                    return "/?readPreference=secondaryPreferred";
                case MongoDbReadPreference.Nearest:
                    return "/?readPreference=mearest";
                case null:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}