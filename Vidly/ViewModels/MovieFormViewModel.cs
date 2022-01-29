using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public Book Book { get; set; }
        public int? Id { get; set; }

        
        [StringLength(255)]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "مصنف")]
        
        public string Author { get; set; }

        [Display(Name = "موضوع")]
        
        public string Topic { get; set; }
        //public byte? GenreId { get; set; }

        //[Display(Name = "Release Date")]
        //[Required]
        //public DateTime? ReleaseDate { get; set; }

        [Display(Name = "کل  تعداد")]
        [Range(1, 20)]
        
        public byte? NumberInStock { get; set; }

        public List<string> TopicsList { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "تبدیل  فارم" : "نئ  کتاب";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Book movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Author = movie.Author;
            //ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            //GenreId = movie.GenreId;
        }
    }
}