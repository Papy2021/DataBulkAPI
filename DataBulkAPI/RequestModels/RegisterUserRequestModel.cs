using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DataBulkAPI.RequestModels
{
    public class RegisterUserRequestModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The Email should contain 8 Caracteres minimum")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password Doesn't Match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
