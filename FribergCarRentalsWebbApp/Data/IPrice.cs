using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface IPrice
    {
        Price? Get(int id);
        Price? Find(Expression<Func<Price, bool>> predicate);
        IEnumerable<Price> All();
        void Add(Price price);
        void Update(Price price);
        void Save();
    }
}
