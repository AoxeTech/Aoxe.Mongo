using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class EnumULongUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumULong.Apple)]
        [InlineData(EnumULong.Banana)]
        [InlineData(EnumULong.Pear)]
        public void EnumULongTest(EnumULong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULong = value
            });
            Assert.Equal(1L, testModel.EnumULong == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumULong);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumULongArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumULongArray = new[] {EnumULong.Apple, EnumULong.Banana, EnumULong.Pear};
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULongArray = enumULongArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumULongArray, modifyModel.EnumULongArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumULongListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumULongList = new List<EnumULong> {EnumULong.Apple, EnumULong.Banana, EnumULong.Pear};
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULongList = enumULongList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumULongList, modifyModel.EnumULongList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumULong.Apple)]
        [InlineData(EnumULong.Banana)]
        [InlineData(EnumULong.Pear)]
        public async Task EnumULongTestAsync(EnumULong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULong = value
            });
            Assert.Equal(1L, testModel.EnumULong == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumULong);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumULongArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumULongArray = new[] {EnumULong.Apple, EnumULong.Banana, EnumULong.Pear};
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULongArray = enumULongArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumULongArray, modifyModel.EnumULongArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumULongListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumULongList = new List<EnumULong> {EnumULong.Apple, EnumULong.Banana, EnumULong.Pear};
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumULongList = enumULongList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumULongList, modifyModel.EnumULongList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}