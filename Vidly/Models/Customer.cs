using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "برائے  مہربانی  اپنا  نام  درج  کریں")]
        [StringLength(255)]
        [Display(Name = "نام")]
        public string Name { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        
        public MembershipType MembershipType { get; set; }

        [Display(Name = "ممبرشپ  ٹائپ")]
        [Required (ErrorMessage = "برائے  مہربانی  ایک  آپشن  سلیکٹ  کریں۔")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "تاریخ  پیدائش")]
        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
        
        
        [Required  (ErrorMessage = "برائے  مہربانی  اپنا  نمبر  درج  کریں")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "برائے  مہربانی  نمبر  صحیح  فارمیٹ  کے  حساب  سے  درج  کریں")]
        [Display(Name = "فون  نمبر")]
        public string Phone { get; set; }
    }
}