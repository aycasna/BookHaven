using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public interface IRecommendationService
    {
        Task<Recommendation> GetRecommendations(string title);
    }
}
