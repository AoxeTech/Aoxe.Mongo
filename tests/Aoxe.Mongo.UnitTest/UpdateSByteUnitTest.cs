namespace Aoxe.Mongo.UnitTest;

public class UpdateSByteUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(sbyte.MinValue)]
    [InlineData(0)]
    [InlineData(sbyte.MaxValue)]
    public void SByteTest(sbyte value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { SByte = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.SByte);
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void SByteArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var sbyteArray = new[] { sbyte.MinValue, sbyte.MaxValue };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { SByteArray = sbyteArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(sbyteArray, modifyModel.SByteArray));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void SByteListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var sbyteList = new List<sbyte> { sbyte.MinValue, sbyte.MaxValue };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { SByteList = sbyteList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(sbyteList, modifyModel.SByteList));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(sbyte.MinValue)]
    [InlineData(0)]
    [InlineData(sbyte.MaxValue)]
    public async Task SByteTestAsync(sbyte value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { SByte = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.SByte);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task SByteArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var sbyteArray = new[] { sbyte.MinValue, sbyte.MaxValue };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { SByteArray = sbyteArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(sbyteArray, modifyModel.SByteArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task SByteListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var sbyteList = new List<sbyte> { sbyte.MinValue, sbyte.MaxValue };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { SByteList = sbyteList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(sbyteList, modifyModel.SByteList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
