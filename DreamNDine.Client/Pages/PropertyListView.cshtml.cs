using AutoMapper;
using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DreamNDine.Client.Pages
{
	public class PropertyListViewModel : PageModel
	{
		private readonly DreamNDineContext _context;
		private readonly HttpClient _httpClient;
		private IHousingService _housingService;
		private IMapper _mapper;

		public List<PropertiesViewModel>? Properties { get; set; }

		public PropertyListViewModel(DreamNDineContext context, HttpClient httpClient, IHousingService housingService, IMapper mapper)
		{
			var httpClientHandler = new HttpClientHandler();
			httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
			var client = new HttpClient(httpClientHandler);
			_httpClient = client;
			_context = context;
			_housingService = housingService;
			_mapper = mapper;
		}

		public void OnGet()
		{
			var propertiesJsonString = HttpContext.Session.GetString("properties");
			if (propertiesJsonString != null)
			{
				Properties = _mapper.Map<List<PropertiesViewModel>>(JsonConvert.DeserializeObject<List<Properties>>(propertiesJsonString));
			}
			}

		public void OnPost()
        {
	        var propertiesJson = HttpContext.Session.GetString("properties");
	        if (propertiesJson != null)
	        {

	        }
        }
	}
}