using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookHaven.Models;

namespace BookHaven.Data.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RecommendationService> _logger;

        public RecommendationService(HttpClient httpClient, ILogger<RecommendationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Recommendation> GetRecommendations(string title)
        {
            //var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/recommend?title={title}");
            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<Recommendation>(content);
            var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/recommend?title={title}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"API Response: {content}");

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(content);

                if (jsonObject == null)
                {
                    _logger.LogWarning("Deserialized JSON object is null.");
                    return null;
                }

                var recommendations = new Recommendation
                {
                    Title = jsonObject.title,
                    RecommendedBooks = jsonObject.recommended_books.ToObject<List<List<object>>>()
                };

                if (recommendations.RecommendedBooks == null)
                {
                    _logger.LogWarning("RecommendedBooks is null after deserialization.");
                }

                return recommendations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing the JSON response.");
                return null;
            }
        }

        
    }
}
