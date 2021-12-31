using System;
using System.Threading.Tasks;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class ArgumentNullUnitTest : BaseUnitTest
    {
        [Fact]
        public void UpdateNullTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            Assert.Throws<ArgumentNullException>("update",
                () => collection.UpdateMany(p => p.DateTime == DateTime.Now, null));
        }

        [Fact]
        public void WhereNullTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            Assert.Throws<ArgumentNullException>("where",
                () => collection.UpdateMany(null, () => new TestModel { DateTime = DateTime.Now }));
        }

        [Fact]
        public async Task UpdateNullTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            await Assert.ThrowsAsync<ArgumentNullException>("update",
                async () => await collection.UpdateManyAsync(p => p.DateTime == DateTime.Now, null));
        }

        [Fact]
        public async Task WhereNullTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            await Assert.ThrowsAsync<ArgumentNullException>("where",
                async () => await collection.UpdateManyAsync(null, () => new TestModel { DateTime = DateTime.Now }));
        }
    }
}