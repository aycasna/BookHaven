using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public class ReadlistsService : IReadlistsService
    {
        private readonly ApplicationDbContext _context;

        public ReadlistsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Readlist readlist)
        {
            _context.Readlists.Add(readlist);
            await _context.SaveChangesAsync();
        }


        IQueryable<Readlist> IReadlistsService.GetAll()
        {
            var applicationDbContext = _context.Readlists;
            return applicationDbContext;
        }
    }
}
