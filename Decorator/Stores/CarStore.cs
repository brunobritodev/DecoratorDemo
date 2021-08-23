using System.Collections.Generic;
using System.Linq;
using Bogus;
using Decorator.Models;

namespace Decorator.Stores
{
    public class CarStore : ICarStore
    {
        public List<Car> CarStorage { get; set; }

        public CarStore()
        {
            var id = 0;
            var testOrders = new Faker<Car>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o => o.Year, f => f.Random.Int(1950, 2020))
                .RuleFor(o => o.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(o => o.Model, f => f.Vehicle.Model());
            CarStorage = testOrders.Generate(10);
        }

        public CarDto List()
        {
            return new CarDto("Database", CarStorage.ToArray());
        }

        public CarDto Get(int userid)
        {
            return new CarDto("Database", CarStorage.FirstOrDefault(f => f.Id == userid));
        }
    }
}