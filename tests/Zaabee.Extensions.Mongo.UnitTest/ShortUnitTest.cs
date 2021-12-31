using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class ShortUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(short.MinValue)]
        [InlineData(0)]
        [InlineData(short.MaxValue)]
        public void ShortTest(short value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                Short = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Short);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ShortArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var shortArray = new short[] { short.MinValue, 0, short.MaxValue };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                ShortArray = shortArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(shortArray, modifyModel.ShortArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ShortListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var shortList = new List<short> { short.MinValue, 0, short.MaxValue };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                ShortList = shortList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(shortList, modifyModel.ShortList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(short.MinValue)]
        [InlineData(0)]
        [InlineData(short.MaxValue)]
        public async Task ShortTestAsync(short value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                Short = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Short);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ShortArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var shortArray = new short[] { short.MinValue, 0, short.MaxValue };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                ShortArray = shortArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(shortArray, modifyModel.ShortArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ShortListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var shortList = new List<short> { short.MinValue, 0, short.MaxValue };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                ShortList = shortList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(shortList, modifyModel.ShortList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}