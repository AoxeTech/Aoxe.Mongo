using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace UnitTest
{
    public class UnitTest
    {
        private readonly IZaabeeMongoClient _client;

        public UnitTest()
        {
            _client = new ZaabeeMongoClient(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
                "TestDB", "", "TestUser", "123"));
//            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.152:27017/TestDB","TestDB");
//            _client = new ZaabeeMongoClient("mongodb://CaiwuR:Bp1qZHMLY9@172.31.200.2:27017/?authSource=FlytOaData","FlytOaData");
        }

        [Fact]
        public void Add()
        {
            var model = GetModel();
            _client.Add(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void AddAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.NotNull(result);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void AddRange()
        {
            var models = GetModels(3).ToList();
            _client.AddRange(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void AddRangeAsync()
        {
            var models = GetModels(4).ToList();
            await _client.AddRangeAsync(models);
            var ids = models.Select(g => g.Id).ToList();
            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id))
                .ToList();
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public void Delete()
        {
            var model = GetModel();
            _client.Add(model);
            Assert.Equal(1L, _client.Delete(model));
        }

        [Fact]
        public async void DeleteAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            Assert.Equal(1L, await _client.DeleteAsync(model));
        }

        [Fact]
        public void DeleteMany()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, _client.Delete<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public async void DeleteManyAsync()
        {
            var models = GetModels(5);
            await _client.AddRangeAsync(models);
            var strings = models.Select(p => p.String);
            Assert.Equal(5L, await _client.DeleteAsync<TestModel>(p => strings.Contains(p.String)));
        }

        [Fact]
        public void Update()
        {
            var model = GetModel();
            _client.Add(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.UtcNow = DateTime.UtcNow;
            _client.Update(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public async void UpdateAsync()
        {
            var model = GetModel();
            await _client.AddAsync(model);
            model.Int = 199;
            model.String = Guid.NewGuid().ToString();
            model.UtcNow = DateTime.UtcNow;
            await _client.UpdateAsync(model);
            var result = _client.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            Assert.Equal(model.ToJson(), result.ToJson());
        }

        [Fact]
        public void UpdateMany()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), Now = DateTime.Now, UtcNow = DateTime.UtcNow}
            };
            var modifyQuantity = _client.Update(
                () => new TestModel
                {
                    Now = now,
                    UtcNow = utcNow,
                    String = name,
                    Kids = kids,
                    TestEnum = TestEnum.Banana
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.Now = now;
                model.UtcNow = utcNow;
                model.String = name;
                model.Kids = kids;
                model.TestEnum = TestEnum.Banana;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        [Fact]
        public async void UpdateManyAsync()
        {
            var models = GetModels(5);
            _client.AddRange(models);
            var strings = models.Select(p => p.String);
            var ids = models.Select(p => p.Id);
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;
            var name = Guid.NewGuid().ToString();
            var kids = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid(), Now = DateTime.Now, UtcNow = DateTime.UtcNow}
            };
            var modifyQuantity = await _client.UpdateAsync(
                () => new TestModel
                {
                    Now = now,
                    UtcNow = utcNow,
                    String = name,
                    Kids = kids
                },
                p => strings.Contains(p.String));
            models.ForEach(model =>
            {
                model.Now = now;
                model.UtcNow = utcNow;
                model.String = name;
                model.Kids = kids;
            });

            var results = _client.GetQueryable<TestModel>().Where(p => ids.Contains(p.Id)).ToList();

            Assert.Equal(5L, modifyQuantity);
            Assert.Equal(models.OrderBy(p => p.Id).ToList().ToJson(), results.OrderBy(p => p.Id).ToList().ToJson());
        }

        private TestModel GetModel(int seed = 0)
        {
            return new TestModel
            {
                Id = Guid.NewGuid(),
                Guid = Guid.NewGuid(),
                Short = -1,
                Int = -2,
                Long = -3,
                Ushort = 1,
                Uint = 2,
                Ulong = 3,
                Float = 0.1F,
                Double = 0.2,
                Decimal = 0.3M,
                Now = DateTime.Now,
                UtcNow = DateTime.UtcNow,
                TestEnum = (TestEnum) (seed % 3),
                String = Guid.NewGuid().ToString(),
                Object = null,
                Kid = new TestModel
                {
                    Id = Guid.NewGuid(),
                    Guid = Guid.NewGuid(),
                    Short = -1,
                    Int = -2,
                    Long = -3,
                    Ushort = 1,
                    Uint = 2,
                    Ulong = 3,
                    Float = 0.1F,
                    Double = 0.2,
                    Decimal = 0.3M,
                    Now = DateTime.Now,
                    UtcNow = DateTime.UtcNow,
                    TestEnum = (TestEnum) (seed % 3),
                    String = string.Empty,
                    Object = null
                },
                Kids = new List<TestModel>
                {
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        Short = -1,
                        Int = -2,
                        Long = -3,
                        Ushort = 1,
                        Uint = 2,
                        Ulong = 3,
                        Float = 0.1F,
                        Double = 0.2,
                        Decimal = 0.3M,
                        Now = DateTime.Now,
                        UtcNow = DateTime.UtcNow,
                        TestEnum = (TestEnum) (seed % 3),
                        String = string.Empty,
                        Object = null
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        Short = -1,
                        Int = -2,
                        Long = -3,
                        Ushort = 1,
                        Uint = 2,
                        Ulong = 3,
                        Float = 0.1F,
                        Double = 0.2,
                        Decimal = 0.3M,
                        Now = DateTime.Now,
                        UtcNow = DateTime.UtcNow,
                        TestEnum = (TestEnum) (seed % 3),
                        String = string.Empty,
                        Object = null
                    },
                    new TestModel
                    {
                        Id = Guid.NewGuid(),
                        Guid = Guid.NewGuid(),
                        Short = -1,
                        Int = -2,
                        Long = -3,
                        Ushort = 1,
                        Uint = 2,
                        Ulong = 3,
                        Float = 0.1F,
                        Double = 0.2,
                        Decimal = 0.3M,
                        Now = DateTime.Now,
                        UtcNow = DateTime.UtcNow,
                        TestEnum = (TestEnum) (seed % 3),
                        String = string.Empty,
                        Object = null
                    },
                }
            };
        }

        private List<TestModel> GetModels(int quantity)
        {
            return Enumerable.Range(0, quantity).Select(GetModel).ToList();
        }
    }
}