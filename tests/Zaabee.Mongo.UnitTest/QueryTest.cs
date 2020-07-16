using System;
using System.Linq;
using Xunit;

namespace Zaabee.Mongo.UnitTest
{
    public class QueryTest : BaseUnitTest
    {
        [Fact]
        public void JoinTest1()
        {
            var testModel = TestModelFactory.GetModel();
            ZaabeeMongoClient.Add(testModel);
            var query = from a in ZaabeeMongoClient.GetQueryable<TestModel>()
                join b in ZaabeeMongoClient.GetQueryable<TestModel>() on a.Id equals b.Id into joinedReadings
                where a.Id == testModel.Id
                select new {A = a, B = joinedReadings};
            Assert.Throws<NotSupportedException>(() => query.FirstOrDefault());
        }

        [Fact]
        public void JoinTest2()
        {
            var query = from a in ZaabeeMongoClient.GetQueryable<TestModel>()
                from b in ZaabeeMongoClient.GetQueryable<TestModel>()
                where a.GuidList.Contains(b.Id)
                select new {a, b};
            Assert.Throws<NotSupportedException>(() => query.ToList());
        }
    }
}