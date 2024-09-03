namespace Aoxe.Mongo.Client.UnitTest;

public class UpdateDateTimeUtcUnitTest : BaseUnitTest
{
    [Fact]
    public void DateTimeUtcTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var value = DateTime.UtcNow.AddDays(1);
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtc = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(value, modifyModel.DateTimeUtc));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void DateTimeUtcArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var dateTimeUtcArray = new[]
        {
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1)
        };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtcArray = dateTimeUtcArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(dateTimeUtcArray, modifyModel.DateTimeUtcArray));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void DateTimeUtcListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var dateTimeUtcList = new List<DateTime>
        {
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1)
        };
        var result = AoxeMongoClient.UpdateMany(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtcList = dateTimeUtcList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(dateTimeUtcList, modifyModel.DateTimeUtcList));
        Assert.Equal(1L, AoxeMongoClient.DeleteMany<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public async Task DateTimeUtcTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var value = DateTime.UtcNow.AddDays(1);
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtc = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(value, modifyModel.DateTimeUtc));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task DateTimeUtcArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var dateTimeUtcArray = new[]
        {
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1)
        };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtcArray = dateTimeUtcArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(dateTimeUtcArray, modifyModel.DateTimeUtcArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task DateTimeUtcListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var dateTimeUtcList = new List<DateTime>
        {
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1)
        };
        var result = await AoxeMongoClient.UpdateManyAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { DateTimeUtcList = dateTimeUtcList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(dateTimeUtcList, modifyModel.DateTimeUtcList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
