namespace BookHaven.Models
{
    public class Book
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public float average_rating { get; set; }
        public string isbn { get; set; }
        public int isbn13 { get; set; }
        public string language_code { get; set; }
        public int num_pages { get; set; }
        public int ratings_count { get; set; }
        public int text_reviews_count { get; set; }
        public DateOnly publication_date { get; set; }
        public string publisher { get; set; }

    }
}
