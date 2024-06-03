namespace BookHaven.Models
{
    public class Book
    {
        public int Id { get; set; } //primary key
        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public int YearOfPublication { get; set; }
        public string Publisher { get; set; }
        public string ImageURLS { get; set; }
        public string ImageURLM { get; set; }
        public string ImageURLL { get; set; }

        public List<Review> Reviews { get; set; }

        // Parameterless constructor required by EF Core
        //private Book() { }

        //// Constructor to set properties
        //public Book(int id, string isbn, string booktitle, string bookauthor, int yearofpublication, string publisher, string imageurls, string imageurlm, string imageurll)
        //{
        //    Id = id;
        //    ISBN = isbn;
        //    BookTitle = booktitle;
        //    BookAuthor = bookauthor;
        //    YearOfPublication = yearofpublication;
        //    Publisher = publisher;
        //    ImageURLS = imageurls;
        //    ImageURLM = imageurlm;
        //    ImageURLL = imageurll;

        //}
    }
}
