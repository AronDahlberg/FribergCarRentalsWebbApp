using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface ICar
    {
        Car? Get(int id);
        Car? Find(Expression<Func<Car, bool>> predicate);
        IEnumerable<Car> All();
        IEnumerable<Car> AllIncludingPricesAndImages();
        void Add(Car car);
        void Update(Car car);
        void Save();
    }
}
