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

		public IList<Properties?> GetAllProperties()
		{
			return _context.Properties.ToList();
		}

		public Properties? AddProperty(Properties? property)
		{
			_context.Properties.Add(property);
			_context.SaveChanges();
			return property;
		}

		public Properties EditProperty(Properties properties)
		{
			var propertyToEdit = _context.Properties.FirstOrDefault(p => p.PropertyID == properties.PropertyID);

			if (propertyToEdit == null) return new Properties();
			propertyToEdit.City = properties.City;
			propertyToEdit.Price = properties.Price;
			propertyToEdit.Description = properties.Description;
			_context.SaveChanges();

			return propertyToEdit;
		}

		public Properties? GetPropertyById(int id)
		{
			return _context.Properties.FirstOrDefault(p => p.PropertyID == id);
		}


		public List<Properties?> GetPropertiesByCityAndTime(string city, DateTime startDate, DateTime endDate)
		{
            // Step 1: Fetch properties by city
			city = city.ToLower();
            var properties = _context.Properties.Where(p => p.Address.Contains(city)|| p.City.Contains(city)|| p.PropertyName.Contains(city)).ToList();
				
			// Step 2: Calculate availability
			var availableProperties = new List<Properties?>();
			foreach (var property in properties)
			{
                List<Booking> overlappingBookings = _context.Bookings
                    .Where(b => b.PropertyID == property.PropertyID &&
                                (b.CheckInDate <= endDate && b.CheckOutDate >= startDate))
                    .ToList() ?? new List<Booking>();

                int occupiedRooms = overlappingBookings.Count();// Calculate occupied rooms based on 'overlappingBookings' ...


				if (property.AvaialableRooms - occupiedRooms > 0)
				{
					availableProperties.Add(property);
				}
			}

			return availableProperties;
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
				throw new Exception($"Properties with ID {id} not found");
			}

			return property;
		}
	}
}
