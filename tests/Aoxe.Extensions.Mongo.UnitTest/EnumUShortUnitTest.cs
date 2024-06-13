using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Aoxe.Extensions.Mongo.UnitTest
{
    public class EnumUShortUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumUShort.Apple)]
        [InlineData(EnumUShort.Banana)]
        [InlineData(EnumUShort.Pear)]
        public void EnumUShortTest(EnumUShort value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShort = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumUShort == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumUShort);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumUShortArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumUShortArray = new[] { EnumUShort.Apple, EnumUShort.Banana, EnumUShort.Pear };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShortArray = enumUShortArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumUShortArray, modifyModel.EnumUShortArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumUShortListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumUShortList = new List<EnumUShort>
            {
                EnumUShort.Apple,
                EnumUShort.Banana,
                EnumUShort.Pear
            };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShortList = enumUShortList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumUShortList, modifyModel.EnumUShortList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumUShort.Apple)]
        [InlineData(EnumUShort.Banana)]
        [InlineData(EnumUShort.Pear)]
        public async Task EnumUShortTestAsync(EnumUShort value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShort = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumUShort == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumUShort);
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumUShortArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumUShortArray = new[] { EnumUShort.Apple, EnumUShort.Banana, EnumUShort.Pear };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShortArray = enumUShortArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumUShortArray, modifyModel.EnumUShortArray));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumUShortListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumUShortList = new List<EnumUShort>
            {
                EnumUShort.Apple,
                EnumUShort.Banana,
                EnumUShort.Pear
            };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumUShortList = enumUShortList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumUShortList, modifyModel.EnumUShortList));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }
    }
}
