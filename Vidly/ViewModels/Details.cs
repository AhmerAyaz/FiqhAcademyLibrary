using Vidly.Models;

namespace Vidly.ViewModels
{
    public class Details
    {
        public Book book { get; set; }
        public int[] genreCount { get; set; }
        public string[] genreName { get; set; }
    }
}