using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Zaabee.Extensions.Mongo.UnitTest
{
    public class ComplexTypeUnitTest : BaseUnitTest
    {
        [Fact]
        public void ComplexTypeTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var value = new TestModel();
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                Kid = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value.ToJson(), modifyModel.Kid.ToJson());
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ComplexTypeInitTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                Kid = new TestModel {String = value}
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new TestModel {String = value}.ToJson(), modifyModel.Kid.ToJson());
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ComplexTypeArrayTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var kidArray = new[]
            {
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()}
            };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                KidArray = kidArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(kidArray.Select(p => p.Id), modifyModel.KidArray.Select(p => p.Id)));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ComplexTypeArrayInitTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                KidArray = new[]
                {
                    new TestModel {String = timeString},
                    new TestModel {String = timeString},
                    new TestModel {String = timeString}
                }
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new[]
            {
                new TestModel {String = timeString},
                new TestModel {String = timeString},
                new TestModel {String = timeString}
            }.ToJson(), modifyModel.KidArray.ToJson());
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ComplexTypeListTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var kidList = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()}
            };
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                KidList = kidList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(kidList.Select(p => p.Id), modifyModel.KidList.Select(p => p.Id)));
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public void ComplexTypeListInitTest()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            collection.InsertOne(testModel);
            var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = collection.UpdateMany(t => t.Id == testModel.Id, () => new TestModel
            {
                KidList = new List<TestModel>
                {
                    new TestModel {String = timeString},
                    new TestModel {String = timeString},
                    new TestModel {String = timeString}
                }
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new List<TestModel>
            {
                new TestModel {String = timeString},
                new TestModel {String = timeString},
                new TestModel {String = timeString}
            }.ToJson(), modifyModel.KidList.ToJson());
            Assert.Equal(1L, collection.DeleteOne(t => t.Id == testModel.Id).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var value = new TestModel();
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                Kid = value
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(value.ToJson(), modifyModel.Kid.ToJson());
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeInitTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                Kid = new TestModel {String = value}
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new TestModel {String = value}.ToJson(), modifyModel.Kid.ToJson());
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeArrayTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var kidArray = new[]
            {
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()}
            };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                KidArray = kidArray
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(kidArray.Select(p => p.Id), modifyModel.KidArray.Select(p => p.Id)));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeArrayInitTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                KidArray = new[]
                {
                    new TestModel {String = timeString},
                    new TestModel {String = timeString},
                    new TestModel {String = timeString}
                }
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new[]
            {
                new TestModel {String = timeString},
                new TestModel {String = timeString},
                new TestModel {String = timeString}
            }.ToJson(), modifyModel.KidArray.ToJson());
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeListTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var kidList = new List<TestModel>
            {
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()},
                new TestModel {Id = Guid.NewGuid()}
            };
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                KidList = kidList
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.True(Comparer.Compare(kidList.Select(p => p.Id), modifyModel.KidList.Select(p => p.Id)));
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }

        [Fact]
        public async Task ComplexTypeListInitTestAsync()
        {
            var collection = MongoDatabase.GetCollection<TestModel>("TestModel");
            var testModel = TestModelFactory.GetModel();
            await collection.InsertOneAsync(testModel);
            var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result = await collection.UpdateManyAsync(t => t.Id == testModel.Id, () => new TestModel
            {
                KidList = new List<TestModel>
                {
                    new TestModel {String = timeString},
                    new TestModel {String = timeString},
                    new TestModel {String = timeString}
                }
            });
            Assert.Equal(1L, result.ModifiedCount);
            var modifyModel = collection.AsQueryable().First(p => p.Id == testModel.Id);
            Assert.Equal(new List<TestModel>
            {
                new TestModel {String = timeString},
                new TestModel {String = timeString},
                new TestModel {String = timeString}
            }.ToJson(), modifyModel.KidList.ToJson());
            Assert.Equal(1L, (await collection.DeleteOneAsync(t => t.Id == testModel.Id)).DeletedCount);
        }
    }
}