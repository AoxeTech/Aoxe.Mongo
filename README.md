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
    private static IZaabeeMongoClient _client;

    public UnitTest()
    {
        _client = new ZaabeeMongoClient(new MongoDbConfiger(new List<string> {"192.168.78.152:27017"},
            "TestDB", "TestUser", "123"));
    }

    [Fact]
    public void Add()
    {
        var model = new TestModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            Name = "Apple"
        };
        _client.Add(model);
    }

    [Fact]
    public void AddRange()
    {
        var datas = new List<TestModel>
        {
            new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 20,
                Name = "Apple"
            },
            new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 21,
                Name = "pear"
            },
            new TestModel
            {
                Id = Guid.NewGuid(),
                Age = 22,
                Name = "banana"
            }
        };
        _client.AddRange(datas);
    }

    [Fact]
    public void Delete()
    {
        var query = _client.GetQueryable<TestModel>();
        var data = query.FirstOrDefault();
        _client.Delete(data);
        _client.Delete<TestModel>(p => p.Name == "banana");
    }

    [Fact]
    public void Update()
    {
        var query = _client.GetQueryable<TestModel>();
        var data = query.First();
        data.Name = Guid.NewGuid().ToString();
        _client.Update(data);
    }
}
```

>Notice:The Zaabee.Mongo will priority select the property named "Id" as the Primary Key.And then find $"{type.Name}Id" like UserId or ProductId witch the class named User or Product.If still not found,the property with KeyAttribute witch in System.ComponentModel.DataAnnotations will be the last selection.
