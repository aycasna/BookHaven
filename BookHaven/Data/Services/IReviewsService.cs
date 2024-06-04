using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public interface IReviewsService
    {
        Task Add(Review review);

        
    }
}
