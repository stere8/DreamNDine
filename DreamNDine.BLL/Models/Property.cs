using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }

        [Required]
        public string PropertyName { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvaialableRooms { get; set; }

        public int OwnerID { get; set; }
        public User Owner { get; set; } // Navigation Property

        public ICollection<string> PropertyPhotos { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}