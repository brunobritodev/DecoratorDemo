using System.Collections.Generic;
using System.Linq;

namespace Decorator.Models
{
    public class CarDto
    {
        public List<Car> Cars { get; set; }
        public string From { get; set; }

        public CarDto(string from, params Car[] cars)
        {
            Cars = cars.ToList();
            From = from;
        }

        public void FromMemory()
        {
            From = "Memory Cache";
        }
    }
}
