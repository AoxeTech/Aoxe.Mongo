namespace Aoxe.Mongo.UnitTest;

public class UpdateComplexTypeUnitTest : BaseUnitTest
{
    [Fact]
    public void ComplexTypeTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var value = new TestModel();
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { Kid = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value.ToJson(), modifyModel.Kid.ToJson());
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ComplexTypeInitTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { Kid = new TestModel { String = value } }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(new TestModel { String = value }.ToJson(), modifyModel.Kid.ToJson());
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ComplexTypeArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var kidArray = new[]
        {
            new TestModel { Id = Guid.NewGuid() },
            new TestModel { Id = Guid.NewGuid() },
            new TestModel { Id = Guid.NewGuid() }
        };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { KidArray = kidArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(
            Comparer.Compare(kidArray.Select(p => p.Id), modifyModel.KidArray.Select(p => p.Id))
        );
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ComplexTypeArrayInitTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () =>
                new TestModel
                {
                    KidArray = new[]
                    {
                        new TestModel { String = timeString },
                        new TestModel { String = timeString },
                        new TestModel { String = timeString }
                    }
                }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(
            new[]
            {
                new TestModel { String = timeString },
                new TestModel { String = timeString },
                new TestModel { String = timeString }
            }.ToJson(),
            modifyModel.KidArray.ToJson()
        );
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ComplexTypeListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var kidList = new List<TestModel>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() }
        };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { KidList = kidList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(
            Comparer.Compare(kidList.Select(p => p.Id), modifyModel.KidList.Select(p => p.Id))
        );
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ComplexTypeListInitTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () =>
                new TestModel
                {
                    KidList = new List<TestModel>
                    {
                        new() { String = timeString },
                        new() { String = timeString },
                        new() { String = timeString }
                    }
                }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(
            new List<TestModel>
            {
                new() { String = timeString },
                new() { String = timeString },
                new() { String = timeString }
            }.ToJson(),
            modifyModel.KidList.ToJson()
        );
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public async Task ComplexTypeTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var value = new TestModel();
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { Kid = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value.ToJson(), modifyModel.Kid.ToJson());
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ComplexTypeInitTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { Kid = new TestModel { String = value } }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(new TestModel { String = value }.ToJson(), modifyModel.Kid.ToJson());
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ComplexTypeArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var kidArray = new[]
        {
            new TestModel { Id = Guid.NewGuid() },
            new TestModel { Id = Guid.NewGuid() },
            new TestModel { Id = Guid.NewGuid() }
        };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { KidArray = kidArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(
            Comparer.Compare(kidArray.Select(p => p.Id), modifyModel.KidArray.Select(p => p.Id))
        );
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ComplexTypeArrayInitTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () =>
                new TestModel
                {
                    KidArray = new[]
                    {
                        new TestModel { String = timeString },
                        new TestModel { String = timeString },
                        new TestModel { String = timeString }
                    }
                }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(
            new[]
            {
                new TestModel { String = timeString },
                new TestModel { String = timeString },
                new TestModel { String = timeString }
            }.ToJson(),
            modifyModel.KidArray.ToJson()
        );
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ComplexTypeListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var kidList = new List<TestModel>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() }
        };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { KidList = kidList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(
            Comparer.Compare(kidList.Select(p => p.Id), modifyModel.KidList.Select(p => p.Id))
        );
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ComplexTypeListInitTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () =>
                new TestModel
                {
                    KidList = new List<TestModel>
                    {
                        new() { String = timeString },
                        new() { String = timeString },
                        new() { String = timeString }
                    }
                }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(
            new List<TestModel>
            {
                new() { String = timeString },
                new() { String = timeString },
                new() { String = timeString }
            }.ToJson(),
            modifyModel.KidList.ToJson()
        );
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
