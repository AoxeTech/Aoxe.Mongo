# Zaabee.Mongo

Mongo repository.

## QuickStart

### NuGet

Install-Package Zaabee.Mongo

### Example

Model

```CSharp
public class TestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

Initialize the client(It is recommended to store a MongoClient instance in a global place, either as a static variable or in an IoC container with a singleton lifetime.)

```CSharp
ZaabeeMongoClient = new ZaabeeMongoClient(new ZaabeeMongoOptions
{
    ConnectionString = "mongodb://admin:123@192.168.78.140:27017/admin?authSource=admin&replicaSet=rs",
    Database = "TestDB"
});
```

Add

```CSharp
mongoClient.Add(model);
mongoClient.AddRange(models);
```

Delete

```CSharp
mongoClient.Delete(model);
mongoClient.Delete<TestModel>(model => names.Contains(model.Name));
```

Update

```CSharp
model.Name = "banana";
mongoClient.Update(model);
mongoClient.Update(() => new TestModel
            {
                Age = 22,
                Name = "pear"
            },
            p => names.Contains(p.Name);)
```

Query

```CSharp
var query = mongoClient.GetQueryable<TestModel>();
var result = query.First(p => p.Name == "pear");
var result = query.Where(p => names.Contains(p.Name)).ToList();
```

>Notice1:The Zaabee.Mongo will give priority to the property with BsonIdAttribute as the Primary Key.If not found,the property which named "Id"/"id"/"_id" will be follow.
>Notice2:TableAttribute in System.ComponentModel.Annotations can be used to mapping the document name.
