using FribergCarRentalsWebbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class CarRepository(ApplicationDbContext context) : ICar
    {
        private readonly ApplicationDbContext _context = context;

        public void Add(Car car)
        {
            throw new NotImplementedException();
        }

        public Car? Find(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> All()
        {
            return _context.Cars.AsEnumerable();
        }

        public IEnumerable<Car> EagerAll()
        {
            return [.. _context.Cars
                .Include(c => c.Prices)
                .Include(c => c.Images)];
        }

        public Car? Get(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }

        public Car? EagerGet(int id)
        {
            return _context.Cars
                    .Include(c => c.Prices)
                    .Include(c => c.Images)
                    .FirstOrDefault(c => c.Id == id);
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
