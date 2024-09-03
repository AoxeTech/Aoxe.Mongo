namespace Aoxe.Mongo.UnitTest;

public class UpdateDecimalUnitTest : BaseUnitTest
{
    public static IEnumerable<object[]> Params = new List<object[]>
    {
        new object[] { decimal.MinValue },
        new object[] { 0 },
        new object[] { decimal.MaxValue }
    };

    [Theory]
    [MemberData(nameof(Params))]
    public void DecimalTest(decimal value)
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { Decimal = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Decimal);
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void DecimalArrayTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var decimalArray = new[] { decimal.MinValue, 0, decimal.MaxValue };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { DecimalArray = decimalArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(decimalArray, modifyModel.DecimalArray));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Fact]
    public void DecimalListTest()
    {
        var testModel = TestModelFactory.GetModel();
        AoxeMongoClient.Add(testModel);
        var decimalList = new List<decimal> { decimal.MinValue, 0, decimal.MaxValue };
        var result = AoxeMongoClient.Update(
            t => t.Id == testModel.Id,
            () => new TestModel { DecimalList = decimalList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(decimalList, modifyModel.DecimalList));
        Assert.Equal(1L, AoxeMongoClient.Delete<TestModel>(t => t.Id == testModel.Id));
    }

    [Theory]
    [MemberData(nameof(Params))]
    public async Task DecimalTestAsync(decimal value)
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { Decimal = value }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.Equal(value, modifyModel.Decimal);
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task DecimalArrayTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var decimalArray = new[] { decimal.MinValue, 0, decimal.MaxValue };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { DecimalArray = decimalArray }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(decimalArray, modifyModel.DecimalArray));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }

    [Fact]
    public async Task DecimalListTestAsync()
    {
        var testModel = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(testModel);
        var decimalList = new List<decimal> { decimal.MinValue, 0, decimal.MaxValue };
        var result = await AoxeMongoClient.UpdateAsync(
            t => t.Id == testModel.Id,
            () => new TestModel { DecimalList = decimalList }
        );
        Assert.Equal(1L, result);
        var modifyModel = AoxeMongoClient
            .GetQueryable<TestModel>()
            .First(p => p.Id == testModel.Id);
        Assert.True(Comparer.Compare(decimalList, modifyModel.DecimalList));
        Assert.Equal(1L, (await AoxeMongoClient.DeleteAsync<TestModel>(t => t.Id == testModel.Id)));
    }
}
