using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Zaabee.Mongo;
using Zaabee.Mongo.Common;

namespace UnitTest
{
    public class UnitTest
    {
        private static MongoDbRepository _mongoDbService;

        public UnitTest()
        {
            _mongoDbService = new MongoDbRepository(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
                "TestDB", "TestUser", "123"));
        }

        [Fact]
        public void Test1()
        {
            var model = new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 20,
                Name = "Apple"
            };
            _mongoDbService.Add(model);
            var result = _mongoDbService.GetQueryable<TestModel>().FirstOrDefault(p => p.Id == model.Id);
            model.Name = "pear";
            var i1 = _mongoDbService.Update(model);
            var i2 = _mongoDbService.Delete(model);
            var i3 = _mongoDbService.Delete<TestModel>(p => p.Id == model.Id);
        }
    }
}