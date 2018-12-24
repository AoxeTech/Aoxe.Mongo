# Zaabee.Mongo

Mongo repository.

## QuickStart

### NuGet

    Install-Package Zaabee.Mongo

### Example

```CSharp
public class TestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class UnitTest
{
    private readonly IZaabeeMongoClient _client;
    private readonly TestModel _model;
    private readonly List<TestModel> _models;

    public UnitTest()
    {
        _client = new ZaabeeMongoClient("mongodb://TestUser:123@192.168.78.152:27017/TestDB","TestDB");
        _model = new TestModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            Name = "Apple"
        };
        _models = Enumerable.Range(0, quantity).Select(p=>new TestModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            Name = "Pear"
        }).ToList();
    }

    [Fact]
    public void Test()
    {
        var names = _models.Select(model => model.Name).ToList();

        _client.Add(_model);
        _client.AddRange(_models);

        var results = _client.GetQueryable<TestModel>().Where(model => model.Age == _model.Age).ToList();

        _model.Name = Guid.NewId().ToString();
        _client.Update(_model);
        _client.Update(() => new TestModel
                {
                    Age = 22,
                    Name = "banana"
                },
                p => strs.Contains(p.String));

        _client.Delete(_model);
        _client.Delete<TestModel>(model => names.Contains(model.Name));
    }
}
```

>Notice:The Zaabee.Mongo will priority select the property named "Id" as the Primary Key.And then find $"{type.Name}Id" like UserId or ProductId witch the class named User or Product.If still not found,the property with KeyAttribute witch in System.ComponentModel.DataAnnotations will be the last selection.
