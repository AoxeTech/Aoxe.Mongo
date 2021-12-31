using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class DateTimeUtcUnitTest : BaseUnitTest
    {
        [Fact]
        public void DateTimeUtcTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var value = DateTime.UtcNow.AddDays(1);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtc = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(value, modifyModel.DateTimeUtc));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void DateTimeUtcArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var dateTimeUtcArray = new[] { DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, DateTime.UtcNow.AddDays(1) };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtcArray = dateTimeUtcArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(dateTimeUtcArray, modifyModel.DateTimeUtcArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void DateTimeUtcListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var dateTimeUtcList = new List<DateTime>
                {DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, DateTime.UtcNow.AddDays(1)};
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtcList = dateTimeUtcList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(dateTimeUtcList, modifyModel.DateTimeUtcList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public async Task DateTimeUtcTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var value = DateTime.UtcNow.AddDays(1);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtc = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(value, modifyModel.DateTimeUtc));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task DateTimeUtcArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var dateTimeUtcArray = new[] { DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, DateTime.UtcNow.AddDays(1) };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtcArray = dateTimeUtcArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(dateTimeUtcArray, modifyModel.DateTimeUtcArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task DateTimeUtcListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var dateTimeUtcList = new List<DateTime>
                {DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, DateTime.UtcNow.AddDays(1)};
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                DateTimeUtcList = dateTimeUtcList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(dateTimeUtcList, modifyModel.DateTimeUtcList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}