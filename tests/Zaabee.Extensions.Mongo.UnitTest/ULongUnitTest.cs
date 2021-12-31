using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    //TODO:BSON doesn't have an unsigned 64 bit integer type.
    public class ULongUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(0)]
        [InlineData(ulong.MaxValue / 2)]
        public void ULongTest(ulong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                ULong = value
            });
            Assert.Equal(1L, testModel.ULong == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.ULong);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ULongArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var ulongArray = new[] { ulong.MinValue, 0UL, ulong.MaxValue / 2 };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                ULongArray = ulongArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(ulongArray, modifyModel.ULongArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ULongListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var ulongList = new List<ulong> { ulong.MinValue, 0UL, ulong.MaxValue / 2 };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                ULongList = ulongList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(ulongList, modifyModel.ULongList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(0)]
        [InlineData(ulong.MaxValue / 2)]
        public async Task ULongTestAsync(ulong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                ULong = value
            });
            Assert.Equal(1L, testModel.ULong == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.ULong);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ULongArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var ulongArray = new[] { ulong.MinValue, 0UL, ulong.MaxValue / 2 };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                ULongArray = ulongArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(ulongArray, modifyModel.ULongArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ULongListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var ulongList = new List<ulong> { ulong.MinValue, 0UL, ulong.MaxValue / 2 };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                ULongList = ulongList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(ulongList, modifyModel.ULongList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}