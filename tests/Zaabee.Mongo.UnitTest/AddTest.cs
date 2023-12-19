using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Xunit;

namespace Zaabee.Mongo.UnitTest
{
    public class AddTest : BaseUnitTest
    {
        [Fact]
        public void Add()
        {
            var model = TestModelFactory.GetModel();
            ZaabeeMongoClient.Add(model);
            var result = ZaabeeMongoClient
                .GetQueryable<TestModel>()
                .FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async Task AddAsync()
        {
            var model = TestModelFactory.GetModel();
            await ZaabeeMongoClient.AddAsync(model);
            var result = ZaabeeMongoClient
                .GetQueryable<TestModel>()
                .FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void AddRange()
        {
            var models = TestModelFactory.GetModels(3).ToList();
            ZaabeeMongoClient.AddRange(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = ZaabeeMongoClient
                .GetQueryable<TestModel>()
                .Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(
                models.OrderBy(p => p.Id).ToList().ToJson(),
                results.OrderBy(p => p.Id).ToList().ToJson()
            );
        }

        [Fact]
        public async Task AddRangeAsync()
        {
            var models = TestModelFactory.GetModels(4).ToList();
            await ZaabeeMongoClient.AddRangeAsync(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = ZaabeeMongoClient
                .GetQueryable<TestModel>()
                .Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(
                models.OrderBy(p => p.Id).ToList().ToJson(),
                results.OrderBy(p => p.Id).ToList().ToJson()
            );
        }
    }
}
