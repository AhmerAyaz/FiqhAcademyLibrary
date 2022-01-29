using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public Book Movie { get; set; }

        public string UserId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime DateReturned { get; set; }
    }
}