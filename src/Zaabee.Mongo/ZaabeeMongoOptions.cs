using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Zaabee.Mongo
{
    public class ZaabeeMongoOptions
    {
        public string ConnectionString { get; set; }
        public MongoUrl MongoUrl { get; set; }
        public MongoClientSettings MongoSettings { get; set; }
        public string Database { get; set; }
        public DateTimeKind DateTimeKind { get; set; } = DateTimeKind.Local;
        public GuidRepresentation GuidRepresentation { get; set; } = GuidRepresentation.CSharpLegacy;
    }
}