using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class DecimalUnitTest : BaseUnitTest
    {
        public static IEnumerable<object[]> Params = new List<object[]>
        {
            new object[] { decimal.MinValue },
            new object[] { 0 },
            new object[] { decimal.MaxValue }
        };

        [Theory]
        [MemberData(nameof(Params))]
        public void DecimalTest(decimal value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { Decimal = value }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Decimal);
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void DecimalArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var decimalArray = new[] { decimal.MinValue, 0, decimal.MaxValue };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { DecimalArray = decimalArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(decimalArray, modifyModel.DecimalArray));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void DecimalListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var decimalList = new List<decimal> { decimal.MinValue, 0, decimal.MaxValue };
            var result = collection.UpdateMany(
                t => t.Id == testModel.Id,
                () => new TestModel { DecimalList = decimalList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(decimalList, modifyModel.DecimalList));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Theory]
        [MemberData(nameof(Params))]
        public async Task DecimalTestAsync(decimal value)
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { Decimal = value }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value, modifyModel.Decimal);
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task DecimalArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var decimalArray = new[] { decimal.MinValue, 0, decimal.MaxValue };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { DecimalArray = decimalArray }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(decimalArray, modifyModel.DecimalArray));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }

        [Fact]
        public async Task DecimalListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var decimalList = new List<decimal> { decimal.MinValue, 0, decimal.MaxValue };
            var result = await collection.UpdateManyAsync(
                t => t.Id == testModel.Id,
                () => new TestModel { DecimalList = decimalList }
            );
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(decimalList, modifyModel.DecimalList));
            Assert.Equal(
                1L,
                (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount
            );
        }
    }
}
