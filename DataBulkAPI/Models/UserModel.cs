using System.ComponentModel.DataAnnotations;

namespace DataBulkAPI.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage ="Name Length is out of range, username should have 50 carateres max")]
        [MinLength(4, ErrorMessage ="The UserName must have 4 caracteres in minimum ")]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="The Email should contain 8 Caracteres minimum")]
        public string? Password { get; set; }

        public string? PhoneNumber { get;set; }
        public string? Role { get; set; }
        public string? Givename { set; get; }
        public string? IsEmailConfirmed { set; get; }

        public int AccessFailCount { get; set; }
        public bool LooOutEnabled { get; set; }
        public DateTime LockOutEnd { get; set; }


    }
}
