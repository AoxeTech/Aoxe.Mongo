using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Zaabee.Mongo.UnitTest
{
    public class DeleteTest : BaseUnitTest
    {
        [Fact]
        public void DeleteSuccess()
        {
            var model = TestModelFactory.GetModel();
            ZaabeeMongoClient.Add(model);
            Assert.Equal(1L, ZaabeeMongoClient.Delete(model));
        }

        [Fact]
        public void DeleteNull()
        {
            Assert.Throws<ArgumentNullException>("entity", () => ZaabeeMongoClient.Delete((TestModel)null));
        }

        [Fact]
        public async Task DeleteSuccessAsync()
        {
            var model = TestModelFactory.GetModel();
            await ZaabeeMongoClient.AddAsync(model);
            Assert.Equal(1L, await ZaabeeMongoClient.DeleteAsync(model));
        }

        [Fact]
        public async Task DeleteNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("entity",
                async () => await ZaabeeMongoClient.DeleteAsync((TestModel)null));
        }

        [Fact]
        public void DeleteManySuccess()
        {
            var models = TestModelFactory.GetModels(5);
            ZaabeeMongoClient.AddRange(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, ZaabeeMongoClient.Delete<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public void DeleteManyNull()
        {
            Assert.Throws<ArgumentNullException>("where",
                () => ZaabeeMongoClient.Delete((Expression<Func<TestModel, bool>>)null));
        }

        [Fact]
        public async Task DeleteManySuccessAsync()
        {
            var models = TestModelFactory.GetModels(5);
            await ZaabeeMongoClient.AddRangeAsync(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, await ZaabeeMongoClient.DeleteAsync<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public async Task DeleteManyNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("where",
                async () => await ZaabeeMongoClient.DeleteAsync((Expression<Func<TestModel, bool>>)null));
        }
    }
}