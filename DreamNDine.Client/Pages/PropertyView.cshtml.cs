using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DreamNDine.Client.Pages
{
	public class PropertyViewModel : PageModel
	{
		private readonly DreamNDineContext _context;
		private readonly HttpClient _httpClient;
		private IHousingService _housingService;

		public List<PropertiesViewModel> Properties { get; set; }

		public PropertyViewModel(DreamNDineContext context, HttpClient httpClient, IHousingService housingService)
		{
			var httpClientHandler = new HttpClientHandler();
			httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
			var client = new HttpClient(httpClientHandler);
			_httpClient = client;
			_context = context;
			_housingService = housingService;
		}

		public async Task OnGet(string location, DateTime? checkInDate, DateTime? checkOutDate)
		{
			var apiBaseUrl = "https://127.0.0.1:5196/api/Property/";
			HttpResponseMessage response;

			if (location == null && checkInDate == null && checkOutDate == null)
			{
				response = await _httpClient.GetAsync(apiBaseUrl);
			}
			else
			{

				var queryParams = $"search?city={location}&startDate={checkInDate}&endDate={checkOutDate}";

				
				response = await _httpClient.GetAsync(apiBaseUrl + queryParams);
			}

			if (response.IsSuccessStatusCode)
			{
				var propertiesViewModels = await response.Content.ReadFromJsonAsync<IEnumerable<PropertiesViewModel>>();
				Properties = propertiesViewModels.ToList();
				//var properties = propertiesViewModels.Select(vm => new Property
				//{
				//	// Map PropertiesViewModel properties to your Property model 
				//	PropertyName = vm.PropertyName,
				//	Address = vm.Address,
				//	AvaialableRooms = vm.AvaialableRooms,
				//	Price = vm.Price,
				//	City = vm.City,
				//	Description = vm.Description,
				//	OwnerID = vm.PropertyId
				//	// ... other mappings
				//}).ToList();
			}
			else
			{
				// Handle API errors (you might want to store an error message)
			}
		}

	}

}

