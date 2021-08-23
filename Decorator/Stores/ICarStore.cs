using Decorator.Models;

namespace Decorator.Stores
{
    public interface ICarStore
    {
        CarDto List();
        CarDto Get(int id);
    }
}