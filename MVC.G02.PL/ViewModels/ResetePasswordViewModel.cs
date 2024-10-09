using System.ComponentModel.DataAnnotations;

namespace MVC.G02.PL.ViewModels
{
    public class ResetePasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password Min Length is 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirmed Password dose not Match Password")]
        public string ConfirmPassword { get; set; }
    }
}
