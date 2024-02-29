using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNDine.BLL.Models;

namespace DreamNDine.BLL.Services
{
    public class HousingServices : IHousingService
    {
        private readonly DbContext.DreamNDineContext _context;

        public HousingServices(DbContext.DreamNDineContext context)
        {
            _context = context;
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _context.Properties;
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
            propertyToEdit.Location = property.Location;
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
            return _context.Properties.Where(p => p.Location == city && p.Bookings.All(b => b.CheckOutDate < startDate || b.CheckInDate > endDate)).ToList();
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
    }
}
