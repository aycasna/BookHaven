using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookHaven.Data;
using BookHaven.Models;
using BookHaven.Data.Services;
using System.Reflection;
using System.Drawing.Printing;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BookHaven.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IReadlistsService _readlistsService;
        private readonly IReviewsService _reviewsService;

        public BooksController(IBooksService booksService, IReadlistsService readlistsService, IReviewsService reviewsService)
        {

            _booksService = booksService;
            _readlistsService = readlistsService;
            _reviewsService = reviewsService;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {

            //var books = await _booksService.GetAll().ToListAsync();
            //if (books == null || !books.Any())
            //{
            //    Console.WriteLine("No books found or unable to fetch books from the database.");
            //    return Problem("No books found or unable to fetch books from the database.");
            //}
            //foreach (var book in books)
            //{
            //    Console.WriteLine($"Book: {book.BookTitle}, Author: {book.BookAuthor}");
            //}
            //return View(books);

            //return _context.Books != null ? 
            //            View(await _context.Books.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Books'  is null.");



            var applicationDbContext = _booksService.GetAll();
            if(!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(b => b.BookTitle.Contains(searchString));
                return View(await applicationDbContext.ToListAsync());
            }
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: AllBooks
        public async Task<IActionResult> AllBooks(int? pageNumber, string searchString)
        {
            var applicationDbContext = _booksService.GetAll();
            int pageSize = 10;
            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(b => b.BookTitle.Contains(searchString));
                return View(await PaginatedList<Book>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            return View("AllBooks", await PaginatedList<Book>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize)); ;

        }

        // GET: AllBooks
        public async Task<IActionResult> Readlist(int? pageNumber)
        {
            var applicationDbContext = _readlistsService.GetAll();
            int pageSize = 10;

            return View(await PaginatedList<Readlist>.CreateAsync(applicationDbContext.Where(r => r.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).Include(r => r.Book).AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View("Readlist", await PaginatedList<Book>.CreateAsync(applicationDbContext.Where(b => b.).AsNoTracking(), pageNumber ?? 1, pageSize)); ;

        }

        [HttpPost]
        public async Task<ActionResult> AddToReadlist([Bind("Id", "isRead", "IdentityUserId", "BookId")] Readlist readlist)
        {
            if(ModelState.IsValid)
            {
                await _readlistsService.Add(readlist);
            }
            
            var book = await _booksService.GetById(readlist.BookId);

            return View("Details", book);

        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _booksService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public async Task<ActionResult> AddReview([Bind("Id, Rating, Content, IdentityUserId, BookId")] Review review)
        {
            if (ModelState.IsValid)
            {
                await _reviewsService.Add(review);
            }
            var book = await _booksService.GetById(review.BookId);
            return View("Details", book);
        }

        //// GET: Books/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Books/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(book);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(book);
        //}

        //// GET: Books/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Books == null)
        //    {
        //        return NotFound();
        //    }

        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(book);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id")] Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(book);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookExists(book.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(book);
        //}

        //// GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Books == null)
        //    {
        //        return NotFound();
        //    }

        //    var book = await _context.Books
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(book);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Books == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
        //    }
        //    var book = await _context.Books.FindAsync(id);
        //    if (book != null)
        //    {
        //        _context.Books.Remove(book);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BookExists(int id)
        //{
        //  return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
