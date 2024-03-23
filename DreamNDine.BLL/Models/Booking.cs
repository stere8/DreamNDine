using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        public int PropertyID { get; set; }

        public int GuestID { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}