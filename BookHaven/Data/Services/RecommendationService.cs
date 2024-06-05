using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BookHaven.Data.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RecommendationService> _logger;
        private readonly ApplicationDbContext _context;

        public RecommendationService(HttpClient httpClient, ILogger<RecommendationService> logger, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _logger = logger;
            _context = context;
        }

        public async Task<Recommendation> GetRecommendations(string title)
        {
            //var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/recommend?title={title}");
            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();
            //_logger.LogInformation($"API Response: {content}");

            //try
            //{
            //    var jsonObject = JsonConvert.DeserializeObject<dynamic>(content);

            //    if (jsonObject == null)
            //    {
            //        _logger.LogWarning("Deserialized JSON object is null.");
            //        return null;
            //    }

            //    var recommendations = new Recommendation
            //    {
            //        Title = jsonObject.title,
            //        RecommendedBooks = jsonObject.recommended_books.ToObject<List<List<object>>>()
            //    };

            //    if (recommendations.RecommendedBooks == null)
            //    {
            //        _logger.LogWarning("RecommendedBooks is null after deserialization.");
            //    }

            //    return recommendations;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error deserializing the JSON response.");
            //    return null;
            //}
            var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/recommend?title={title}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"API Response: {content}");

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<RecommendationResponse>(content);

                if (jsonObject == null || jsonObject.recommended_books == null)
                {
                    _logger.LogWarning("Deserialized JSON object is null or has no recommended_books.");
                    return null;
                }

                var recommendedTitles = jsonObject.recommended_books.Select(x => x[0].ToString()).ToList();
                var books = await _context.Books.Where(b => recommendedTitles.Contains(b.BookTitle)).ToListAsync();

                var recommendations = new Recommendation
                {
                    Title = jsonObject.title,
                    RecommendedBooks = books
                };

                return recommendations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing the JSON response.");
                return null;
            }
        }
        private class RecommendationResponse
        {
            public string title { get; set; }
            public List<List<object>> recommended_books { get; set; }
        }


    }
}
