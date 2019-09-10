using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;

namespace TestProject
{
    public class DeleteTest
    {
        private readonly IZaabeeMongoClient _client;

        public DeleteTest()
        {
            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.143:27017/TestDB/?readPreference=primary", "TestDB");
        }

        [Fact]
        public void DeleteSuccess()
        {
            var model = new TestModelFactory().GetModel();
            _client.Add(model);
            Assert.Equal(1L, _client.Delete(model));
        }

        [Fact]
        public void DeleteNull()
        {
            Assert.Throws<ArgumentNullException>("entity", () => _client.Delete((TestModel) null));
        }

        [Fact]
        public async void DeleteSuccessAsync()
        {
            var model = new TestModelFactory().GetModel();
            await _client.AddAsync(model);
            Assert.Equal(1L, await _client.DeleteAsync(model));
        }

        [Fact]
        public async void DeleteNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("entity",
                async () => await _client.DeleteAsync((TestModel) null));
        }

        [Fact]
        public void DeleteManySuccess()
        {
            var models = new TestModelFactory().GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, _client.Delete<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public void DeleteManyNull()
        {
            Assert.Throws<ArgumentNullException>("where",
                () => _client.Delete((Expression<Func<TestModel, bool>>) null));
        }

        [Fact]
        public async void DeleteManySuccessAsync()
        {
            var models = new TestModelFactory().GetModels(5);
            await _client.AddRangeAsync(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, await _client.DeleteAsync<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public async void DeleteManyNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("where",
                async () => await _client.DeleteAsync((Expression<Func<TestModel, bool>>) null));
        }
    }
}