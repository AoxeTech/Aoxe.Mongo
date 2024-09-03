namespace Aoxe.Mongo.UnitTest;

public class DeleteTest : BaseUnitTest
{
    [Fact]
    public void DeleteSuccess()
    {
        var model = TestModelFactory.GetModel();
        AoxeMongoClient.Add(model);
        Assert.Equal(1L, AoxeMongoClient.Delete(model));
    }

    [Fact]
    public async Task DeleteSuccessAsync()
    {
        var model = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(model);
        Assert.Equal(1L, await AoxeMongoClient.DeleteAsync(model));
    }

    [Fact]
    public void DeleteManySuccess()
    {
        var models = TestModelFactory.GetModels(5);
        AoxeMongoClient.AddRange(models);
        var strings = models.Select(p => p.String);
        Assert.Equal(5L, AoxeMongoClient.Delete<TestModel>(p => strings.Contains(p.String)));
    }

    [Fact]
    public async Task DeleteManySuccessAsync()
    {
        var models = TestModelFactory.GetModels(5);
        await AoxeMongoClient.AddRangeAsync(models);
        var strings = models.Select(p => p.String);
        Assert.Equal(
            5L,
            await AoxeMongoClient.DeleteAsync<TestModel>(p => strings.Contains(p.String))
        );
    }
}
