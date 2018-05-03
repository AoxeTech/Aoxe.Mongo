using System;
using System.Collections.Generic;

namespace Zaabee.Mongo.Common
{
    public class MongoDbConfiger
    {
        public MongoDbConfiger(List<string> hosts, string database, string userName, string password,
            MongoDbReadPreference? readPreference = null, int maxConnectionPoolSize = 100,
            int minConnectionPoolSize = 0, GuidType guidType = GuidType.CSharpLegacy)
        {
            Hosts = hosts;
            Database = database;
            UserName = userName;
            Password = password;
            ReadPreference = readPreference;
            MaxConnectionPoolSize = maxConnectionPoolSize;
            MinConnectionPoolSize = minConnectionPoolSize;
            GuidType = guidType;
        }

        private List<string> _hosts;

        public List<string> Hosts
        {
            get => _hosts ?? (_hosts = new List<string>());
            private set => _hosts = value;
        }

        public string Database { get; }

        public string UserName { get; }

        public string Password { get; }
        
        public int MaxConnectionPoolSize { get;}
        
        public int MinConnectionPoolSize { get;}

        public MongoDbReadPreference? ReadPreference { get; }
        
        public GuidType GuidType { get; }

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