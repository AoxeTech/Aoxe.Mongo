namespace Aoxe.Mongo.UnitTest;

public class UpdateEnumSByteUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(EnumSByte.Apple)]
    [InlineData(EnumSByte.Banana)]
    [InlineData(EnumSByte.Pear)]
    public void EnumSByteTest(EnumSByte value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByte = value }
        );
        Assert.Equal(testModel.EnumSByte == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumSByte);
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumSByteArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumSByteArray = new[] { EnumSByte.Apple, EnumSByte.Banana, EnumSByte.Pear };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByteArray = enumSByteArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumSByteArray, modifyModel.EnumSByteArray));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumSByteListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumSByteList = new List<EnumSByte>
        {
            EnumSByte.Apple,
            EnumSByte.Banana,
            EnumSByte.Pear
        };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByteList = enumSByteList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumSByteList, modifyModel.EnumSByteList));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(EnumSByte.Apple)]
    [InlineData(EnumSByte.Banana)]
    [InlineData(EnumSByte.Pear)]
    public async Task EnumSByteTestAsync(EnumSByte value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByte = value }
        );
        Assert.Equal(testModel.EnumSByte == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumSByte);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumSByteArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumSByteArray = new[] { EnumSByte.Apple, EnumSByte.Banana, EnumSByte.Pear };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByteArray = enumSByteArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumSByteArray, modifyModel.EnumSByteArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumSByteListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumSByteList = new List<EnumSByte>
        {
            EnumSByte.Apple,
            EnumSByte.Banana,
            EnumSByte.Pear
        };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumSByteList = enumSByteList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumSByteList, modifyModel.EnumSByteList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
