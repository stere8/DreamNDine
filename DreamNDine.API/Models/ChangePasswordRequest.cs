using System.ComponentModel.DataAnnotations;

namespace DreamNDine.API.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }

}
