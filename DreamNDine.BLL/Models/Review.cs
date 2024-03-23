using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int PropertyID { get; set; }
        public Properties Properties { get; set; }

        public int GuestID { get; set; }
        public User Guest { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string ReviewText { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}