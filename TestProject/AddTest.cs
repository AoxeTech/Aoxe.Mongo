using System;
using System.Linq;
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
            _client = new ZaabeeMongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"), "TestDB");
        }

        [Fact]
        public void Add()
        {
            var model = new GetModelService().GetModel();
            _client.Add(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void AddAsync()
        {
            var model = new GetModelService().GetModel();
            await _client.AddAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void AddRange()
        {
            var models = new GetModelService().GetModels(3).ToList();
            _client.AddRange(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void AddRangeAsync()
        {
            var models = new GetModelService().GetModels(4).ToList();
            await _client.AddRangeAsync(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }
    }
}