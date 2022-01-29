using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "میل")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "پاسورڈ")]
        public string Password { get; set; }

        [Display(Name = "یاد  رکھیں؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }  //Not used in register View

        [Required(ErrorMessage = "برائے  مہربانی  اپنا  نام  درج  کریں")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "برائے  مہربانی  اپنے  والد  کا  نام  درج  کریں")]
        [Display(Name = "والد  کا  نام")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "برائے  مہربانی  اپنی  میل  درج  کریں")]
        [EmailAddress]
        [Display(Name = "میل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "برائے  مہربانی  اپنا  پاسورڈ  درج  کریں")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "پاسورڈ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کنفرم  پاسورڈ")]
        [Compare("Password", ErrorMessage = "پاسورڈ  اور  کنفرم  پاسورڈ  مختلف  ہیں۔")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "برائے  مہربانی  اپنا  نمبر  درج  کریں")]
        [StringLength(50)]
        [Display(Name = "نمبر")]
        public string Phone { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
