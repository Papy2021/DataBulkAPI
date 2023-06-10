using System.ComponentModel.DataAnnotations;

namespace DataBulkAPI.Models
{
    public class ActorModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Please provide a FullName")]
        [MaxLength(70, ErrorMessage ="Name length out of range")]
        public string? FullName { get; set; }

        
        public string? Gender { get; set; }

        [Required(ErrorMessage ="Please provide an email address")]
        [EmailAddress(ErrorMessage ="Wrong email address format")]
        public string? Email { get; set; }


        [Required(ErrorMessage ="Provide a position for this member")]
        public string? Position { get; set;}



        [Phone(ErrorMessage ="Wrong format for the phone Number")]
        public string? Phone { get; set;}
    }
}
