using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class GuidUnitTest : BaseUnitTest
    {
        [Fact]
        public void GuidTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var guid = Guid.NewGuid();
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { Guid = guid }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(guid, modifyModel.Guid);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void GuidArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var guidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { GuidArray = guidArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(guidArray, modifyModel.GuidArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void GuidListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var guidList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { GuidList = guidList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(guidList, modifyModel.GuidList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public async Task GuidTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var guid = Guid.NewGuid();
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { Guid = guid }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(guid, modifyModel.Guid);
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task GuidArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var guidArray = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { GuidArray = guidArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(guidArray, modifyModel.GuidArray));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task GuidListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var guidList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { GuidList = guidList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(guidList, modifyModel.GuidList));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }
    }
}
