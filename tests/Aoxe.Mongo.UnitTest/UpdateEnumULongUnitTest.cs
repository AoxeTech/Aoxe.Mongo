namespace Aoxe.Mongo.UnitTest;

public class UpdateEnumULongUnitTest : BaseUnitTest
{
    [Theory]
    [InlineData(EnumULong.Apple)]
    [InlineData(EnumULong.Banana)]
    [InlineData(EnumULong.Pear)]
    public void EnumULongTest(EnumULong value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULong = value }
        );
        Assert.Equal(testModel.EnumULong == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumULong);
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumULongArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumULongArray = new[] { EnumULong.Apple, EnumULong.Banana, EnumULong.Pear };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULongArray = enumULongArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumULongArray, modifyModel.EnumULongArray));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void EnumULongListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var enumULongList = new List<EnumULong>
        {
            EnumULong.Apple,
            EnumULong.Banana,
            EnumULong.Pear
        };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULongList = enumULongList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumULongList, modifyModel.EnumULongList));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [InlineData(EnumULong.Apple)]
    [InlineData(EnumULong.Banana)]
    [InlineData(EnumULong.Pear)]
    public async Task EnumULongTestAsync(EnumULong value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULong = value }
        );
        Assert.Equal(testModel.EnumULong == value ? 0L : 1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.EnumULong);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumULongArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumULongArray = new[] { EnumULong.Apple, EnumULong.Banana, EnumULong.Pear };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULongArray = enumULongArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumULongArray, modifyModel.EnumULongArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task EnumULongListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var enumULongList = new List<EnumULong>
        {
            EnumULong.Apple,
            EnumULong.Banana,
            EnumULong.Pear
        };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { EnumULongList = enumULongList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(enumULongList, modifyModel.EnumULongList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
