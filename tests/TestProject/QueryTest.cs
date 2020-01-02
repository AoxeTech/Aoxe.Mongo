using System;
using System.Linq;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;

namespace TestProject
{
    public class QueryTest
    {
        private readonly IZaabeeMongoClient _client;

        public QueryTest()
        {
            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.142:27017/admin/?readPreference=primary",
                "TestDB");
        }

        [Fact]
        public void JoinTest1()
        {
            var queryable = from a in _client.GetQueryable<TestModel>()
                join b in _client.GetQueryable<TestModel>() on a.Id equals b.Id
                select new {a, b};
            var result = queryable.ToList();
        }

        [Fact]
        public void JoinTest2()
        {
            var query = from a in _client.GetQueryable<TestModel>()
                from b in _client.GetQueryable<TestModel>()
                where a.Guids.Contains(b.Id)
                select new {a, b};
            Assert.Throws<NotSupportedException>(() => query.ToList());
        }
    }
}