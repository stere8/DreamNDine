using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class User
    {
        [Key]  // Mark the primary key
        public int UserID { get; set; }

        [Required] // Mark as required
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // We'll address hashing later

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string Phone { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}