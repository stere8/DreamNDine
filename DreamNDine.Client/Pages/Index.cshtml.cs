using DreamNDine.BLL.Services;
using DreamNDine.BLL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DreamNDine.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHousingService _propertyService;

        public IndexModel(ILogger<IndexModel> logger,IHousingService housingService)
        {
            _logger = logger;
            _propertyService = housingService;
        }

        public List<Property> Properties { get; set; }

        public void OnGet()
        {
        }

    }
}