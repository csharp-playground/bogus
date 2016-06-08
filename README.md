## Try Bogus

https://github.com/bchavez/Bogus

```csharp
public enum CustomerType { New, Continue };

public class Customer {
    public CustomerType CustomerType { set; get; }
    public String Name { set; get; }
    public String Address { set; get; }
    public int Id { set; get; }
}

public class BogusSpec {

    [Test]
    public void ShouldGenerateCustomer() {
        var fake = new Faker<Customer>()
            .StrictMode(true)
            .RuleFor(x => x.CustomerType, (f) => f.PickRandom(new[] { CustomerType.New, CustomerType.Continue }))
            .RuleFor(x => x.Name, (f) => f.Name.FirstName())
            .RuleFor(x => x.Id, (f) => f.UniqueIndex)
            .RuleFor(x => x.Address, (f) => f.Address.City());

        var datas = new[] {
            fake.Generate(),
            fake.Generate(),
            fake.Generate()
        };

        datas.Select(x => x.Id).Distinct().Count().Should().Equals(3);
    }
}
```