namespace Aoxe.Mongo.Client.UnitTest;

public class UpdateShortUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(short.MinValue)]
    [InlineData(0)]
    [InlineData(short.MaxValue)]
    public void ShortTest(short value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { Short = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Short);
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ShortArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var shortArray = new short[] { short.MinValue, 0, short.MaxValue };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { ShortArray = shortArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(shortArray, modifyModel.ShortArray));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void ShortListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var shortList = new List<short> { short.MinValue, 0, short.MaxValue };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { ShortList = shortList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(shortList, modifyModel.ShortList));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(short.MinValue)]
    [InlineData(0)]
    [InlineData(short.MaxValue)]
    public async Task ShortTestAsync(short value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { Short = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Short);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ShortArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var shortArray = new short[] { short.MinValue, 0, short.MaxValue };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { ShortArray = shortArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(shortArray, modifyModel.ShortArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task ShortListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var shortList = new List<short> { short.MinValue, 0, short.MaxValue };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { ShortList = shortList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(shortList, modifyModel.ShortList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
