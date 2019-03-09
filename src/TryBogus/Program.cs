using System;
using Bogus.DataSets;
using Bogus.Extensions;
using System.Linq;
using Bogus;
using DynamicTables;

namespace TryBogus {

    public enum CustomerType { New, Continue };

    public class Customer {
        public CustomerType CustomerType { set; get; }
        public String Name { set; get; }
        public String Address { set; get; }
        public String Company { set; get; }
        public int Id { set; get; }
    }

    public class Program {


        public static Customer[] Generate() {
            var company = new Bogus.DataSets.Company(locale: "en");
            var name = new Bogus.DataSets.Name("en");
            var address = new Bogus.DataSets.Address("en_GB");

            var fake = new Faker<Customer>("en")
                .StrictMode(true)
                .RuleFor(x => x.CustomerType, (f) => f.PickRandom(new[] { CustomerType.New, CustomerType.Continue }))
                .RuleFor(x => x.Name, (f) => name.FullName())
                .RuleFor(x => x.Id, (f) => f.UniqueIndex)
                .RuleFor(x => x.Company, f => {
                    return company.CompanyName();
                })
                .RuleFor(x => x.Address, (f) => address.City());

            var datas = new[] {
                fake.Generate(),
                fake.Generate(),
                fake.Generate(),
                fake.Generate(),
                fake.Generate(),
                fake.Generate(),
                fake.Generate()
            };

            return datas;
        }

        static void Main(string[] args) {
            var data = Generate();
            DynamicTable.From(data).Write();

        }
    }
}

