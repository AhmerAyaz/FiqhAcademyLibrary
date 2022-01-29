using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        
        [StringLength(255)]
        [Required(ErrorMessage = "کتاب  کا  نام")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "مصنف  کا  نام")]
        [Display(Name = "مصنف")]
        public string Author { get; set; }
        
        [Display(Name = "مترجم")]
        public string Translator { get; set; }
        

        [Display(Name = "مکتبہ")]
        public string Publisher { get; set; }

        [Display(Name = "موضوع")]
        public Genre Genre { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "موضوع  کا  نام")]
        public byte GenreId { get; set; }

        [Display(Name = "ایڈیشن")]
        //[Required]
        public byte? Edition { get; set; }

        [Display(Name = "جلدیں")]
        public byte? Set { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "قیمت")]
        public int? Price { get; set; }



        //[Display(Name = "Release Date")]
        //public DateTime ReleaseDate { get; set; }

        //[Display(Name = "Number in Stock")]
        //[Range(1, 20)]
        [Display(Name = "تعداد")]
        public byte? NumberInStock { get; set; } //Convert To Int Late

        public byte? NumberAvailable { get; set; }
        public string ReferenceNo { get; set; }
        public byte? SetNo { get; set; }
    }
}