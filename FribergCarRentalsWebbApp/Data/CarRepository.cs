using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class CarRepository : ICar
    {
        public void Add(Car car)
        {
            throw new NotImplementedException();
        }

        public Car? Find(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Car? Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
