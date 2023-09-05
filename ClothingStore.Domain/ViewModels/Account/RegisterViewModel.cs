using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter username")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        [MinLength(5, ErrorMessage ="Password has to have at least 5 symbols")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password" , ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { get; set; }
    }
}