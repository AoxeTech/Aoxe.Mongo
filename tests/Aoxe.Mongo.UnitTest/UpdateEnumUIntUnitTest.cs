namespace Aoxe.Mongo.UnitTest;

public class UpdateEnumUIntUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(EnumUInt.Apple)]
    [InlineData(EnumUInt.Banana)]
    [InlineData(EnumUInt.Pear)]
    public void EnumUIntTest(EnumUInt value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUInt = value }
        );
        Assert.Equal(testModel.EnumUInt == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumUInt);
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumUIntArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumUIntArray = new[] { EnumUInt.Apple, EnumUInt.Banana, EnumUInt.Pear };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUIntArray = enumUIntArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUIntArray, modifyModel.EnumUIntArray));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumUIntListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumUIntList = new List<EnumUInt> { EnumUInt.Apple, EnumUInt.Banana, EnumUInt.Pear };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUIntList = enumUIntList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUIntList, modifyModel.EnumUIntList));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(EnumUInt.Apple)]
    [InlineData(EnumUInt.Banana)]
    [InlineData(EnumUInt.Pear)]
    public async Task EnumUIntTestAsync(EnumUInt value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUInt = value }
        );
        Assert.Equal(testModel.EnumUInt == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumUInt);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumUIntArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumUIntArray = new[] { EnumUInt.Apple, EnumUInt.Banana, EnumUInt.Pear };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUIntArray = enumUIntArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUIntArray, modifyModel.EnumUIntArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumUIntListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumUIntList = new List<EnumUInt> { EnumUInt.Apple, EnumUInt.Banana, EnumUInt.Pear };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUIntList = enumUIntList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUIntList, modifyModel.EnumUIntList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
