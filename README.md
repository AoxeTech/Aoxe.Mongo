# Aoxe.Mongo

English | [简体中文](README-zh_CN.md)

Provide an easy way to use Mongo as repository and with UpdateMany / DeleteMany in lambda style.

This project has implemented three packages:

- Aoxe.Mongo.Abstractions - The abstractions of aoxe mongo.
- Aoxe.Mongo.Client - The implements of Aoxe.Mongo.Abstractions.
- Aoxe.Mongo - The service provider extensions for Aoxe.Mongo.Client to simplify the IOC register.

---

## QuickStart

Install the package from NuGet

```bash
PM> Install-Package Aoxe.Mongo
```

Register the AoxeMongoClient into IOC

```csharp
serviceCollection.AddAoxeMongo("mongodb://localhost:27017", "test");
```

Inject the IAoxeMongoClient by reference Aoxe.Email.Abstractions.

```bash
PM> Install-Package Aoxe.Mongo.Abstractions
```

```csharp
public class TestRepository
{
    private readonly IAoxeMongoClient _mongoClient;

    public TestRepository(IAoxeMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }
}
```

If you don't use IOC, you can instantiate AoxeMongoClient directly.

```bash
PM> Install-Package Aoxe.Mongo.Client
```

```csharp
var mongoClient = new AoxeMongoClient("mongodb://localhost:27017", "test");
```

Now we have a Model class like this:

```csharp
public class TestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

And then we can use the mongo like a repository:

Add

```csharp
mongoClient.Add(model);
mongoClient.AddRange(models);

await mongoClient.AddAsync(model);
await mongoClient.AddRangeAsync(models);
```

Delete

```csharp
mongoClient.Delete(model);
mongoClient.DeleteMany<TestModel>(m => m.Id == model.Id);

await mongoClient.DeleteAsync(model);
await mongoClient.DeleteManyAsync<TestModel>(m => m.Id == model.Id);
```

Update

```csharp
model.Name = "banana";
mongoClient.Update(model);
mongoClient.UpdateMany(() =>
    m => m.Id == model.Id,
    new TestModel
    {
        Age = 22,
        Name = "pear"
    })

await mongoClient.UpdateAsync(model);
await mongoClient.UpdateManyAsync(() =>
    m => m.Id == model.Id,
    new TestModel
    {
        Age = 22,
        Name = "pear"
    })
```

Query

```csharp
var query = mongoClient.GetQueryable<TestModel>();
var result = query.FirstOrDefault(p => p.Name == "pear");
var result = query.Where(p => names.Contains(p.Name)).ToList();
```
