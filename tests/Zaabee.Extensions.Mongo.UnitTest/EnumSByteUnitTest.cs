using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class EnumSByteUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(EnumSByte.Apple)]
        [InlineData(EnumSByte.Banana)]
        [InlineData(EnumSByte.Pear)]
        public void EnumSByteTest(EnumSByte value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByte = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumSByte == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumSByte);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumSByteArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumSByteArray = new[] { EnumSByte.Apple, EnumSByte.Banana, EnumSByte.Pear };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByteArray = enumSByteArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumSByteArray, modifyModel.EnumSByteArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void EnumSByteListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var enumSByteList = new List<EnumSByte>
            {
                EnumSByte.Apple,
                EnumSByte.Banana,
                EnumSByte.Pear
            };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByteList = enumSByteList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumSByteList, modifyModel.EnumSByteList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [InlineData(EnumSByte.Apple)]
        [InlineData(EnumSByte.Banana)]
        [InlineData(EnumSByte.Pear)]
        public async Task EnumSByteTestAsync(EnumSByte value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByte = value }
            );
            Assert.Equal(
                1L,
                testModel.EnumSByte == value ? result.MatchedCount : result.ModifiedCount
            );
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.EnumSByte);
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumSByteArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumSByteArray = new[] { EnumSByte.Apple, EnumSByte.Banana, EnumSByte.Pear };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByteArray = enumSByteArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumSByteArray, modifyModel.EnumSByteArray));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task EnumSByteListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var enumSByteList = new List<EnumSByte>
            {
                EnumSByte.Apple,
                EnumSByte.Banana,
                EnumSByte.Pear
            };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { EnumSByteList = enumSByteList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(enumSByteList, modifyModel.EnumSByteList));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }
    }
}
