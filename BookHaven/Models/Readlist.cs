using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHaven.Models
{
    public class Readlist
    {
        public int Id { get; set; }
        public bool isRead { get; set; } = false;

        [Required]
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser User { get; set; }

        public int BookId { get; set; } // Book Id
        [ForeignKey("BookId")]
        public Book Book { get; set; }

    }
}
