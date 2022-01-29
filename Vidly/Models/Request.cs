using System;

namespace Vidly.Models
{
    public class Request
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}