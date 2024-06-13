using System;
using System.Linq;
using Xunit;

namespace Aoxe.Mongo.UnitTest
{
    public class QueryTest : BaseUnitTest
    {
        [Fact]
        public void JoinTest1()
        {
            var testModel = TestModelFactory.GetModel();
            AoxeMongoClient.Add(testModel);
            var query =
                from a in AoxeMongoClient.GetQueryable<TestModel>()
                join b in AoxeMongoClient.GetQueryable<TestModel>()
                    on a.Id equals b.Id
                    into joinedReadings
                where a.Id == testModel.Id
                select new { A = a, B = joinedReadings };
            Assert.Throws<NotSupportedException>(() => query.FirstOrDefault());
        }

        [Fact]
        public void JoinTest2()
        {
            var query =
                from a in AoxeMongoClient.GetQueryable<TestModel>()
                from b in AoxeMongoClient.GetQueryable<TestModel>()
                where a.GuidList.Contains(b.Id)
                select new { a, b };
            Assert.Throws<NotSupportedException>(() => query.ToList());
        }
    }
}
