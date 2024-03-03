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
        public string City { get; set; }

        public string? Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvaialableRooms { get; set; }

        public int OwnerID { get; set; }
        public string MainPic { get; set; }
        public string OtherPics { get; set; }
    }
}