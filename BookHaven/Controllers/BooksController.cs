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

namespace BookHaven.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
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
            return View(await applicationDbContext.ToListAsync());
        }

        //// GET: Books/Details/5
        //public async Task<IActionResult> Details(int? id)
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
