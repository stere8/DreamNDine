using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamNDine.BLL.Services
{
    public class BookingService : IBookingServices
    {
        private readonly DreamNDineContext _context;

        public BookingService(DreamNDineContext context)
        {
            _context = context;
        }

        public Booking CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.BookingID == id);
        }

        public IEnumerable<Booking> GetBookingsByGuestId(int id)
        {
            return _context.Bookings.Where(b => b.GuestID == id);
        }

        public Booking CanceBooking(Booking booking)
        {
            var bookingToCancel = _context.Bookings.FirstOrDefault(b => b.BookingID == booking.BookingID);
            
            if (bookingToCancel == null)
            {
                return null;
            }

            bookingToCancel.BookingStatus = BookingStatus.Cancelled;
            _context.SaveChanges();
            return bookingToCancel;
        }

        public bool IsBookingPaid(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingID == id);
            return booking.PaymentStatus == PaymentStatus.Paid;
        }

        public bool IsPropertyAvailableForDates(int propertyId, DateTime startDate, DateTime endDate)
        {
            var property = _context.Properties.Include(p => p.Bookings)
                .FirstOrDefault(p => p.PropertyID == propertyId);

            if (property == null) return false;  // Property not found

            // Check if any existing booking overlaps with the requested dates
            var hasConflictingBooking = property.Bookings.Any(b => b.CheckInDate <= endDate && b.CheckOutDate >= startDate);

            // Ensure AvailableRooms is sufficient
            return !hasConflictingBooking && property.AvaialableRooms >= 1;
        }

    }
}