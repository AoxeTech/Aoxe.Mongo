using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Aoxe.Mongo.UnitTest
{
    public class DeleteTest : BaseUnitTest
    {
        [Fact]
        public void DeleteSuccess()
        {
            var model = TestModelFactory.GetModel();
            AoxeMongoClient.Add(model);
            Assert.Equal(1L, AoxeMongoClient.Delete(model));
        }

        [Fact]
        public void DeleteNull()
        {
            Assert.Throws<ArgumentNullException>(
                "entity",
                () => AoxeMongoClient.Delete((TestModel)null)
            );
        }

        [Fact]
        public async Task DeleteSuccessAsync()
        {
            var model = TestModelFactory.GetModel();
            await AoxeMongoClient.AddAsync(model);
            Assert.Equal(1L, await AoxeMongoClient.DeleteAsync(model));
        }

        [Fact]
        public async Task DeleteNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                "entity",
                async () => await AoxeMongoClient.DeleteAsync((TestModel)null)
            );
        }

        [Fact]
        public void DeleteManySuccess()
        {
            var models = TestModelFactory.GetModels(5);
            AoxeMongoClient.AddRange(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, AoxeMongoClient.Delete<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public void DeleteManyNull()
        {
            Assert.Throws<ArgumentNullException>(
                "where",
                () => AoxeMongoClient.Delete((Expression<Func<TestModel, bool>>)null)
            );
        }

        [Fact]
        public async Task DeleteManySuccessAsync()
        {
            var models = TestModelFactory.GetModels(5);
            await AoxeMongoClient.AddRangeAsync(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(
                5L,
                await AoxeMongoClient.DeleteAsync<TestModel>(p => strings.Contains(p.String))
            );
        }

        [Fact]
        public async Task DeleteManyNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                "where",
                async () =>
                    await AoxeMongoClient.DeleteAsync((Expression<Func<TestModel, bool>>)null)
            );
        }
    }
}
