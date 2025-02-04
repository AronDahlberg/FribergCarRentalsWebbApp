using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface IAdministrator
    {
        Administrator? Find(Expression<Func<Administrator, bool>> predicate);
        Administrator? GetById(int id);
    }
}
