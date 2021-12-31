using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class EnumIntUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumInt.Apple)]
        [InlineData(EnumInt.Banana)]
        [InlineData(EnumInt.Pear)]
        public void EnumIntTest(EnumInt value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumInt = value
            });
            Assert.Equal(1L, testModel.EnumInt == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumInt);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumIntConstantTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumInt = EnumInt.Apple
            });
            Assert.Equal(1L, testModel.EnumInt == EnumInt.Apple ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(EnumInt.Apple, modifyModel.EnumInt);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumIntArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumIntArray = new[] { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumIntArray = enumIntArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntArray, modifyModel.EnumIntArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumIntListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumIntList = new List<EnumInt> { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumIntList = enumIntList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntList, modifyModel.EnumIntList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumInt.Apple)]
        [InlineData(EnumInt.Banana)]
        [InlineData(EnumInt.Pear)]
        public async Task EnumIntTestAsync(EnumInt value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumInt = value
            });
            Assert.Equal(1L, testModel.EnumInt == value ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumInt);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumIntConstantTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumInt = EnumInt.Apple
            });
            Assert.Equal(1L, testModel.EnumInt == EnumInt.Apple ? result.MatchedCount : result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(EnumInt.Apple, modifyModel.EnumInt);
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumIntArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumIntArray = new[] { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumIntArray = enumIntArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntArray, modifyModel.EnumIntArray));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task EnumIntListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumIntList = new List<EnumInt> { EnumInt.Apple, EnumInt.Banana, EnumInt.Pear };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                EnumIntList = enumIntList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumIntList, modifyModel.EnumIntList));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}