namespace Aoxe.Mongo.Client.UnitTest;

public class UpdateEnumUShortUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(EnumUShort.Apple)]
    [InlineData(EnumUShort.Banana)]
    [InlineData(EnumUShort.Pear)]
    public void EnumUShortTest(EnumUShort value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShort = value }
        );
        Assert.Equal(testModel.EnumUShort == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumUShort);
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumUShortArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumUShortArray = new[] { EnumUShort.Apple, EnumUShort.Banana, EnumUShort.Pear };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShortArray = enumUShortArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUShortArray, modifyModel.EnumUShortArray));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumUShortListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumUShortList = new List<EnumUShort>
        {
            EnumUShort.Apple,
            EnumUShort.Banana,
            EnumUShort.Pear
        };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShortList = enumUShortList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUShortList, modifyModel.EnumUShortList));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(EnumUShort.Apple)]
    [InlineData(EnumUShort.Banana)]
    [InlineData(EnumUShort.Pear)]
    public async Task EnumUShortTestAsync(EnumUShort value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShort = value }
        );
        Assert.Equal(testModel.EnumUShort == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumUShort);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumUShortArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumUShortArray = new[] { EnumUShort.Apple, EnumUShort.Banana, EnumUShort.Pear };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShortArray = enumUShortArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUShortArray, modifyModel.EnumUShortArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumUShortListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumUShortList = new List<EnumUShort>
        {
            EnumUShort.Apple,
            EnumUShort.Banana,
            EnumUShort.Pear
        };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumUShortList = enumUShortList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumUShortList, modifyModel.EnumUShortList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
