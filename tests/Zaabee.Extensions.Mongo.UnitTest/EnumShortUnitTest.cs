using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class EnumShortUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumShort.Apple)]
        [InlineData(EnumShort.Banana)]
        [InlineData(EnumShort.Pear)]
        public void EnumShortTest(EnumShort value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShort = value
            });
            Assert.Equal(1L, testModel.EnumShort == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumShort);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumShortArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumShortArray = new[] { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShortArray = enumShortArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortArray, modifyModel.EnumShortArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumShortListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumShortList = new List<EnumShort> { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShortList = enumShortList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortList, modifyModel.EnumShortList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumShort.Apple)]
        [InlineData(EnumShort.Banana)]
        [InlineData(EnumShort.Pear)]
        public async Task EnumShortTestAsync(EnumShort value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShort = value
            });
            Assert.Equal(1L, testModel.EnumShort == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumShort);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumShortArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumShortArray = new[] { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShortArray = enumShortArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortArray, modifyModel.EnumShortArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumShortListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumShortList = new List<EnumShort> { EnumShort.Apple, EnumShort.Banana, EnumShort.Pear };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumShortList = enumShortList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumShortList, modifyModel.EnumShortList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}