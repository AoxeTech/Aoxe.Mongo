using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class QueryUnitTest : BaseUnitTest
    {
        [Fact]
        public void JoinTest()
        {
            var testModels = TestModelFactory.GetModels(3);
            var testKidModels = TestModelFactory.GetKidModels(2, testModels[0].Id);
            testKidModels.AddRange(TestModelFactory.GetKidModels(3, testModels[1].Id));
            testKidModels.AddRange(TestModelFactory.GetKidModels(4, testModels[2].Id));

            var modelCollection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var kidModelCollection = MongoDatabase.GetCollection<KidTestModel>("KidTestModel");

            modelCollection.InsertMany(testModels);
            kidModelCollection.InsertMany(testKidModels);

            var testModel = testModels.First();
            var id = testModel.Id;

            var query = from a in modelCollection.AsQueryable()
                        join b in kidModelCollection.AsQueryable() on a.Id equals b.ParentId into joinedReadings
                        where a.Id == id
                        select new { A = a.Id, Kids = joinedReadings };

            var result = query.First();
            Assert.Equal(testModel.Id, result.A);
            Assert.Equal(testKidModels.Where(p => p.ParentId == testModel.Id).ToList().ToJson(), result.Kids.ToJson());
        }
    }
}