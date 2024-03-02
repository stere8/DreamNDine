using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNDine.BLL.Models;

namespace DreamNDine.BLL.Services
{
    public class HousingService : IHousingService
    {
        private readonly DbContext.DreamNDineContext _context;

        public HousingService(DbContext.DreamNDineContext context)
        {
            _context = context;
        }

        public IList<Property> GetAllProperties()
        {
            return _context.Properties.ToList();
        }

        public Property AddProperty(Property property)
        {
            _context.Properties.Add(property);
            _context.SaveChanges();
            return property;
        }

        public Property EditProperty(Property property)
        {
            var propertyToEdit = _context.Properties.FirstOrDefault(p => p.PropertyID == property.PropertyID);
            
            if (propertyToEdit == null) return new Property();
            propertyToEdit.City = property.City;
            propertyToEdit.Price = property.Price;
            propertyToEdit.Description = property.Description;
            _context.SaveChanges();

            return propertyToEdit;
        }

        public Property GetPropertyById(int id)
        {
            return _context.Properties.FirstOrDefault(p => p.PropertyID == id);
        }

        public List<Property> GetPropertiesByCityAndTime(string city, DateTime startDate, DateTime endDate)
        {
            return _context.Properties
                .Join(_context.Bookings,  // Join with Bookings
                    p => p.PropertyID,   // Property foreign key
                    b => b.PropertyID,   // Booking matching key
                    (p, b) => new { Property = p, Booking = b }) // Result selector
                .Where(pb => pb.Property.City == city &&
                             pb.Booking.CheckOutDate < startDate ||
                             pb.Booking.CheckInDate > endDate)
                .Select(pb => pb.Property) // Select the Property
                .ToList();
        }

        public bool DeleteProperty(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyID == id);
            if (property == null) return false;
            _context.Properties.Remove(property);
            _context.SaveChanges();
            return true;
        }  

        public decimal GetTotalCost(int propertyID, DateTime startDate, DateTime endDate)
        {
            var property = _context.Properties.FirstOrDefault(p => p.PropertyID == propertyID);
            var totalCost = property.Price * (endDate - startDate).Days;
            return totalCost;
        }

        public int GetPropertyOwner(int id)
        {
            var property = _context.Properties
                .Where(p => p.PropertyID == id)
                .Select(p => p.OwnerID)
                .FirstOrDefault();

            if (property == 0) // Default value for int, assuming 0 isn't a valid OwnerID 
            {
                throw new Exception($"Property with ID {id} not found");
            }

            return property;
        }
    }
}
