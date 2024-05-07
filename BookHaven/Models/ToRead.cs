using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHaven.Models
{
    public class ToRead
    {
        public int Id { get; set; }
        public bool isRead { get; set; }

        [Required]
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public int bookID { get; set; }
        [ForeignKey("bookID")]


    }
}
