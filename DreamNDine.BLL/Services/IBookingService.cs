using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNDine.BLL.Models;

namespace DreamNDine.BLL.Services
{
    public interface IBookingService
    {
        Booking CreateBooking(Booking booking);
        Booking GetBookingById(int id);
        IEnumerable<Booking> GetBookingsByGuestId(int id);
        Booking CanceBooking(Booking booking);
        bool IsBookingPaid(int id);

        bool IsPropertyAvailableForDates(int propertyID, DateTime startDate, DateTime endDate);
    }
}
