using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class PriceRepository : IPrice
    {
        public void Add(Price price)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Price> All()
        {
            throw new NotImplementedException();
        }

        public Price? Find(Expression<Func<Price, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Price? Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Price price)
        {
            throw new NotImplementedException();
        }
    }
}
