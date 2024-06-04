using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly ApplicationDbContext _context;

        public ReviewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
