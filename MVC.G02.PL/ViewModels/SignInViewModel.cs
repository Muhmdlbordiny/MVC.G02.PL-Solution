using System.ComponentModel.DataAnnotations;

namespace MVC.G02.PL.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password Min Length is 5")]
        [MaxLength(20)]
        public string Password { get; set; }
        public bool Rememberme {  get; set; }
    }
}
