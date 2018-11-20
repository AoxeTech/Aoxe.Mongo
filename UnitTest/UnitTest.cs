using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Abstractions;
using Zaabee.Mongo.Common;

namespace UnitTest
{
    public class UnitTest
    {
        private static IZaabeeMongoClient _client;

        public UnitTest()
        {
//            _client = new ZaabeeMongoClient(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
//                "TestDB", "", "TestUser", "123"));
            _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.152:27017/TestDB","TestDB");
        }

        [Fact]
        public void Add()
        {
            var model = new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 77,
                Name = "Apple",
                CreateTime = DateTime.Now,
                CreateUtcTime = DateTime.UtcNow
            };
            _client.Add(model);
        }

        [Fact]
        public async void AddAsync()
        {
            var model = new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 20,
                Name = "Apple",
                CreateTime = DateTime.Now,
                CreateUtcTime = DateTime.UtcNow
            };
            await _client.AddAsync(model);
        }

        [Fact]
        public void AddRange()
        {
            var datas = new List<TestModel>
            {
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 20,
                    Name = "Apple",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                },
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 21,
                    Name = "pear",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                },
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 22,
                    Name = "banana",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                }
            };
            _client.AddRange(datas);
        }

        [Fact]
        public async void AddRangeAsync()
        {
            var datas = new List<TestModel>
            {
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 20,
                    Name = "Apple",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                },
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 21,
                    Name = "pear",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                },
                new TestModel
                {
                    Id = Guid.NewGuid(),
                    Age = 22,
                    Name = "banana",
                    CreateTime = DateTime.Now,
                    CreateUtcTime = DateTime.UtcNow
                }
            };
            await _client.AddRangeAsync(datas);
        }

        [Fact]
        public void Delete()
        {
            var query = _client.GetQueryable<TestModel>();
            var data = query.FirstOrDefault();
            _client.Delete(data);
            _client.Delete<TestModel>(p => p.Name == "banana");
        }

        [Fact]
        public async void DeleteAsync()
        {
            var query = _client.GetQueryable<TestModel>();
            var data = query.FirstOrDefault();
            await _client.DeleteAsync(data);
            await _client.DeleteAsync<TestModel>(p => p.Name == "banana");
        }

        [Fact]
        public void Update()
        {
            var query = _client.GetQueryable<TestModel>();
            var data = query.First();
            data.Name = Guid.NewGuid().ToString();
            _client.Update(data);
        }

        [Fact]
        public async void UpdateAsync()
        {
            var query = _client.GetQueryable<TestModel>();
            var data = query.First();
            data.Name = Guid.NewGuid().ToString();
            await _client.UpdateAsync(data);
        }
    }
}