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
        IList<Properties?> GetAllProperties();
        Properties? AddProperty(Properties? property);
        Properties EditProperty(Properties properties);
        Properties? GetPropertyById(int id);
        bool DeleteProperty(int id);
        List<Properties?> GetPropertiesByCityAndTime(string city,DateTime startDate, DateTime endDate);
        decimal GetTotalCost(int propertyID, DateTime startDate, DateTime endDate);
        int GetPropertyOwner(int id);
    }
}