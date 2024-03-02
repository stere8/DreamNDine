using System.ComponentModel.DataAnnotations;

namespace DreamNDine.Api.Models
{
    public class UserUpdateRequest
    {
        [EmailAddress] // Optional in case the user wants to update the email
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // To Allow password updates (add password validation if needed)
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}