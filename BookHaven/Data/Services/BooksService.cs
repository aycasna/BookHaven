using BookHaven.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Data.Services
{
    public class BooksService : IBooksService
    {
        private readonly ApplicationDbContext _context;

        public BooksService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> GetAll()
        {
            //return _context.Books != null ?
            //              View(await _context.Books.ToListAsync()) :
            //              Problem("entity set 'applicationdbcontext.books'  is null.");

            //return _context.Books;

            var applicationDbContext = _context.Books;
            return applicationDbContext;
        }
    }
}
