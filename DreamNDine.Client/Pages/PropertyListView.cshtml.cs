using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DreamNDine.Client.Pages
{
	public class PropertyListViewModel : PageModel
	{
		private readonly DreamNDineContext _context;
		private readonly HttpClient _httpClient;
		private IHousingService _housingService;

		public List<PropertiesViewModel>? Properties { get; set; }

		public PropertyListViewModel(DreamNDineContext context, HttpClient httpClient, IHousingService housingService, List<PropertiesViewModel> properties)
		{
			var httpClientHandler = new HttpClientHandler();
			httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
			var client = new HttpClient(httpClientHandler);
			_httpClient = client;
			_context = context;
			_housingService = housingService;
			Properties = TempData["SearchResults"] as List<PropertiesViewModel>;
		}

		public void OnGet()
		{
			
		}

	}

}

