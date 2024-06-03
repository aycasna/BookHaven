using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public interface IBooksService
    {
        IQueryable<Book> GetAll();
    }
}
