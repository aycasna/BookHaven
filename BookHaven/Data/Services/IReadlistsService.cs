using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public interface IReadlistsService
    {
        Task Add(Readlist readlist);

        IQueryable<Readlist> GetAll();
    }
}
