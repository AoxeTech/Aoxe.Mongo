using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Aoxe.Extensions.Mongo.UnitTest
{
    public class EnumLongUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumLong.Apple)]
        [InlineData(EnumLong.Banana)]
        [InlineData(EnumLong.Pear)]
        public void EnumLongTest(EnumLong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLong = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumLong == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumLong);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumLongArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumLongArray = new[] { EnumLong.Apple, EnumLong.Banana, EnumLong.Pear };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongArray = enumLongArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongArray, modifyModel.EnumLongArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumLongListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumLongList = new List<EnumLong>
            {
                EnumLong.Apple,
                EnumLong.Banana,
                EnumLong.Pear
            };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongList = enumLongList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongList, modifyModel.EnumLongList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumLong.Apple)]
        [InlineData(EnumLong.Banana)]
        [InlineData(EnumLong.Pear)]
        public async Task EnumLongTestAsync(EnumLong value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLong = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumLong == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumLong);
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumLongArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumLongArray = new[] { EnumLong.Apple, EnumLong.Banana, EnumLong.Pear };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongArray = enumLongArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongArray, modifyModel.EnumLongArray));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumLongListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumLongList = new List<EnumLong>
            {
                EnumLong.Apple,
                EnumLong.Banana,
                EnumLong.Pear
            };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumLongList = enumLongList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumLongList, modifyModel.EnumLongList));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }
    }
}
