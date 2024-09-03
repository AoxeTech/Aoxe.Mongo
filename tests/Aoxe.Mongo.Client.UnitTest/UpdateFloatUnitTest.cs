namespace Aoxe.Mongo.Client.UnitTest;

public class UpdateFloatUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0)]
    [InlineData(float.MaxValue)]
    public void FloatTest(float value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { Float = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Float);
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void FloatArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var floatArray = new[] { float.MinValue, 0, float.MaxValue };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { FloatArray = floatArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(floatArray, modifyModel.FloatArray));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void FloatListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var floatList = new List<float> { float.MinValue, 0, float.MaxValue };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { FloatList = floatList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(floatList, modifyModel.FloatList));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0)]
    [InlineData(float.MaxValue)]
    public async Task FloatTestAsync(float value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { Float = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Float);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task FloatArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var floatArray = new[] { float.MinValue, 0, float.MaxValue };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { FloatArray = floatArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(floatArray, modifyModel.FloatArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task FloatListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var floatList = new List<float> { float.MinValue, 0, float.MaxValue };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { FloatList = floatList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(floatList, modifyModel.FloatList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
