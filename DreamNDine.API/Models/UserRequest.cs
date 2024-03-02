using System.ComponentModel.DataAnnotations;

namespace DreamNDine.Api.Models
{
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        // other user details for registration, as needed
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}