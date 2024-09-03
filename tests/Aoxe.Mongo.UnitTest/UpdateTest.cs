namespace Aoxe.Mongo.UnitTest;

public class UpdateTest : BaseUnitTest
{
    [Fact]
    public void UpdateSuccess()
    {
        var model = TestModelFactory.GetModel();
        AoxeMongoClient.Add(model);
        model.Int = 199;
        model.String = Guid.NewGuid().ToString();
        model.DateTimeUtc = DateTime.UtcNow;
        AoxeMongoClient.Update(model);
        var result = AoxeMongoClient
            .GetQueryable<TestModel>()
            .FirstOrDefault(p => p.Id == model.Id);
        Assert.Equal(model.ToJson(), result.ToJson());
    }

    [Fact]
    public async Task UpdateSuccessAsync()
    {
        var model = TestModelFactory.GetModel();
        await AoxeMongoClient.AddAsync(model);
        model.Int = 199;
        model.String = Guid.NewGuid().ToString();
        model.DateTimeUtc = DateTime.UtcNow;
        await AoxeMongoClient.UpdateAsync(model);
        var result = AoxeMongoClient
            .GetQueryable<TestModel>()
            .FirstOrDefault(p => p.Id == model.Id);
        Assert.Equal(model.ToJson(), result.ToJson());
    }

    [Fact]
    public void UpdateManySuccess()
    {
        var models = TestModelFactory.GetModels(5);
        AoxeMongoClient.AddRange(models);
        var strings = models.Select(p => p.String);
        var ids = models.Select(p => p.Id);
        var now = DateTime.Now;
        var utcNow = DateTime.UtcNow;
        var name = Guid.NewGuid().ToString();
        var kids = new List<TestModel>
        {
            new TestModel
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                DateTimeUtc = DateTime.UtcNow
            }
        };
        var modifyQuantity = AoxeMongoClient.Update(
            p => strings.Contains(p.String),
            () =>
                new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    KidList = kids,
                    EnumInt = EnumInt.Banana
                }
        );
        models.ForEach(model =>
        {
            model.DateTime = now;
            model.DateTimeUtc = utcNow;
            model.String = name;
            model.KidList = kids;
            model.EnumInt = EnumInt.Banana;
        });

        var results = AoxeMongoClient
            .GetQueryable<TestModel>()
            .Where(p => ids.Contains(p.Id))
            .ToList();

        Assert.Equal(5L, modifyQuantity);
        Assert.Equal(
            models.OrderBy(p => p.Id).ToList().ToJson(),
            results.OrderBy(p => p.Id).ToList().ToJson()
        );
    }

    [Fact]
    public async Task UpdateManySuccessAsync()
    {
        var models = TestModelFactory.GetModels(5);
        await AoxeMongoClient.AddRangeAsync(models);
        var strings = models.Select(p => p.String);
        var ids = models.Select(p => p.Id);
        var now = DateTime.Now;
        var utcNow = DateTime.UtcNow;
        var name = Guid.NewGuid().ToString();
        var kids = new List<TestModel>
        {
            new TestModel
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                DateTimeUtc = DateTime.UtcNow
            }
        };
        var modifyQuantity = await AoxeMongoClient.UpdateAsync(
            p => strings.Contains(p.String),
            () =>
                new TestModel
                {
                    DateTime = now,
                    DateTimeUtc = utcNow,
                    String = name,
                    KidList = kids
                }
        );
        models.ForEach(model =>
        {
            model.DateTime = now;
            model.DateTimeUtc = utcNow;
            model.String = name;
            model.KidList = kids;
        });

        var results = AoxeMongoClient
            .GetQueryable<TestModel>()
            .Where(p => ids.Contains(p.Id))
            .ToList();

        Assert.Equal(5L, modifyQuantity);
        Assert.Equal(
            models.OrderBy(p => p.Id).ToList().ToJson(),
            results.OrderBy(p => p.Id).ToList().ToJson()
        );
    }
}
