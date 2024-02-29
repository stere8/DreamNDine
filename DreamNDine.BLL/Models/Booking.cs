using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        public int PropertyID { get; set; }
        public Property Property { get; set; }

        public int GuestID { get; set; }
        public User Guest { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public decimal TotalCost { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}