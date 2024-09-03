namespace Aoxe.Mongo.Client.UnitTest;

public class UpdateStringUnitTest : BaseUnitTest
{
    [Fact]
    public void StringTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var value = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { String = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.String);
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void StringArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var stringArray = new[]
        {
            Guid.NewGuid().ToString(),
            0.ToString(),
            Guid.NewGuid().ToString()
        };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { StringArray = stringArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(stringArray, modifyModel.StringArray));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void StringListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var stringList = new List<string>
        {
            Guid.NewGuid().ToString(),
            0.ToString(),
            Guid.NewGuid().ToString()
        };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { StringList = stringList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(stringList, modifyModel.StringList));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public async Task StringTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var value = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { String = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.String);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task StringArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var stringArray = new[]
        {
            Guid.NewGuid().ToString(),
            0.ToString(),
            Guid.NewGuid().ToString()
        };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { StringArray = stringArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(stringArray, modifyModel.StringArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task StringListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var stringList = new List<string>
        {
            Guid.NewGuid().ToString(),
            0.ToString(),
            Guid.NewGuid().ToString()
        };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { StringList = stringList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(stringList, modifyModel.StringList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
