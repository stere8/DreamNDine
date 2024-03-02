namespace DreamNDine.Api.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Optional (if you introduce roles)
        public string Role { get; set; }
    }
}