using BookHaven.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task<Book> GetById(int? id)
        {
            var book = await _context.Books
                .Include(b => b.Reviews)
                .ThenInclude(b => b.User)//user of a review
                .FirstOrDefaultAsync(m => m.Id == id);
            return book;
        }
    }
}
