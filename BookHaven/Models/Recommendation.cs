using System.Collections.Generic;
namespace BookHaven.Models
{
    public class Recommendation
    {
        public string Title { get; set; }
        //public List<List<object>> RecommendedBooks { get; set; }
        public List<Book> RecommendedBooks { get; set; }
    }
}
