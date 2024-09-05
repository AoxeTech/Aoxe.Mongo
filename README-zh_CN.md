# Aoxe.Mongo

[English](README.md) | 简体中文

提供一种简单的方法，将 Mongo 用作存储库，并以 lambda 风格使用 UpdateMany / DeleteMany。

该项目实现了三个软件包：

- Aoxe.Mongo.Abstractions - aoxe mongo 的抽象。
- Aoxe.Mongo.Client - Aoxe.Mongo.Abstractions 的实现。
- Aoxe.Mongo - 为 Aoxe.Mongo.Client 提供服务扩展，以简化 IOC 注册。

---

## QuickStart

从 NuGet 安装软件包

```bash
PM> Install-Package Aoxe.Mongo
```

将 AoxeMongoClient 注册到 IOC 容器里

```csharp
serviceCollection.AddAoxeMongo("mongodb://localhost:27017", "test");
```

通过引用 Aoxe.Email.Abstractions 注入 IAoxeMongoClient。

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

不使用 IOC 的话可以直接实例化 AoxeMongoClient 对象

```bash
PM> Install-Package Aoxe.Mongo.Client
```

```csharp
var mongoClient = new AoxeMongoClient("mongodb://localhost:27017", "test");
```

现在假设我们有一个这样的 Model class:

```csharp
public class TestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

然后我们就可以像使用存储库一样使用 mongo:

新增

```csharp
mongoClient.Add(model);
mongoClient.AddRange(models);

await mongoClient.AddAsync(model);
await mongoClient.AddRangeAsync(models);
```

删除

```csharp
mongoClient.Delete(model);
mongoClient.DeleteMany<TestModel>(m => m.Name == "apple");

await mongoClient.DeleteAsync(model);
await mongoClient.DeleteManyAsync<TestModel>(m => m.Name == "apple");
```

修改

```csharp
model.Name = "banana";
mongoClient.Update(model);
mongoClient.UpdateMany(() =>
    m => m.Name == "apple",
    new TestModel
    {
        Age = 22,
        Name = "pear"
    })

await mongoClient.UpdateAsync(model);
await mongoClient.UpdateManyAsync(() =>
    m => m.Name == "apple",
    new TestModel
    {
        Age = 22,
        Name = "pear"
    })
```

查询

```csharp
var query = mongoClient.GetQueryable<TestModel>();
var result = query.FirstOrDefault(p => p.Name == "pear");
var result = query.Where(p => names.Contains(p.Name)).ToList();
```
