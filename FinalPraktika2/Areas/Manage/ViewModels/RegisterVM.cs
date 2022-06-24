using System.ComponentModel.DataAnnotations;

namespace FinalPraktika2.Areas.Manage.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Duzgun daxil edin!!!")]
        public string ConfirmPassword { get; set; }
    }
}
