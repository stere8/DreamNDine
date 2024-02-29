using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNDine.BLL.Models;

namespace DreamNDine.BLL.Services
{
    public interface IHousingService
    {
        IEnumerable<Property> GetAllProperties();
        Property AddProperty(Property property);
        Property EditProperty(Property property);
        Property GetPropertyById(int id);
        bool DeleteProperty(int id);
        List<Property> GetPropertiesByCityAndTime(string city,DateTime startDate, DateTime endDate);
        decimal GetTotalCost(int propertyID, DateTime startDate, DateTime endDate);
    }
}