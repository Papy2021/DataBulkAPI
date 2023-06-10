using System.ComponentModel.DataAnnotations;

namespace DataBulkAPI.RequestModels
{
    public class UserLoginRequestModel
    {
        [Required]
        public string? Username { get; set; }



        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
