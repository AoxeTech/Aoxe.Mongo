using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace TestProject
{
    public class UpdateTest
    {
        private readonly IZaabeeMongoClient _client;

        public UpdateTest()
        {
            _client = new ZaabeeMongoClient(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
                "TestDB", "", "TestUser", "123"));
        }

        [Fact]
        public void UpdateSuccess()
        {
            var model = new GetModelService().GetModel();
            _client.Add(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.DateTimeUtc = DateTime.UtcNow;
            _client.Update(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void UpdateNull()
        {
            Assert.Throws<ArgumentNullException>("entity", () => _client.Update((TestModel) null));
        }

        [Fact]
        public async void UpdateSuccessAsync()
        {
            var model = new GetModelService().GetModel();
            await _client.AddAsync(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.DateTimeUtc = DateTime.UtcNow;
            await _client.UpdateAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void UpdateNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("entity",
                async () => await _client.UpdateAsync((TestModel) null));
        }

        [Fact]
        public void UpdateManySuccess()
        {
            var models = new GetModelService().GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), DateTime = DateTime.Now, DateTimeUtc = DateTime.UtcNow}
            };
            var modifyQuantity = _client.Update(
                () => new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    Kids = kids,
                    EnumInt = EnumInt.Banana
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.DateTime = now;
                model.DateTimeUtc = utcNow;
                model.String = name;
                model.Kids = kids;
                model.EnumInt = EnumInt.Banana;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public void UpdateManyNull()
        {
            Assert.Throws<ArgumentNullException>("update",
                () => _client.Update<TestModel>(null, p => p.DateTime == DateTime.Now));
            Assert.Throws<ArgumentNullException>("where",
                () => _client.Update(() => new TestModel {DateTime = DateTime.Now}, null));
        }

        [Fact]
        public async void UpdateManySuccessAsync()
        {
            var models = new GetModelService().GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), DateTime = DateTime.Now, DateTimeUtc = DateTime.UtcNow}
            };
            var modifyQuantity = await _client.UpdateAsync(
                () => new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    Kids = kids
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.DateTime = now;
                model.DateTimeUtc = utcNow;
                model.String = name;
                model.Kids = kids;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void UpdateManyNullAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>("update",
                async () => await _client.UpdateAsync<TestModel>(null, p => p.DateTime == DateTime.Now));
            await Assert.ThrowsAsync<ArgumentNullException>("where",
                async () => await _client.UpdateAsync(() => new TestModel {DateTime = DateTime.Now}, null));
        }
    }
}