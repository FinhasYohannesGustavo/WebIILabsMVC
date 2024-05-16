using System.ComponentModel.DataAnnotations;

namespace Web_II_Labs.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter your Email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter your Password")]
        [DataType(DataType.Password)]
        public string Password{get;set;}

        [Required(ErrorMessage = "Please Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
