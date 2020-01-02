using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;

namespace TestProject
{
    public class AddTest
    {
        private readonly IZaabeeMongoClient _client;

        public AddTest()
        {
            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.142:27017/admin/?readPreference=primary", "TestDB");
        }

        [Fact]
        public void Add()
        {
            var model = new TestModelFactory().GetModel();
            _client.Add(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async Task AddAsync()
        {
            var model = new TestModelFactory().GetModel();
            await _client.AddAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void AddRange()
        {
            var models = new TestModelFactory().GetModels(3).ToList();
            _client.AddRange(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async Task AddRangeAsync()
        {
            var models = new TestModelFactory().GetModels(4).ToList();
            await _client.AddRangeAsync(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }
    }
}