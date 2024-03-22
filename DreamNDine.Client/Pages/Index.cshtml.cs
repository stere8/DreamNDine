using System.Text;
using System.Text.Json;
using DreamNDine.API.Models;
using DreamNDine.BLL.Services;
using DreamNDine.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DreamNDine.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHousingService _propertyService;
        public PropertySearchModel SearchModel;

        public IndexModel(ILogger<IndexModel> logger,IHousingService housingService)
        {
            _logger = logger;
            _propertyService = housingService;
        }

        public List<Property> Properties { get; set; }

        public async Task<IActionResult> OnPost(PropertySearchModel searchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize(searchData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://127.0.0.1:1026/api/Property/search/", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var properties = JsonSerializer.Deserialize<List<Property>>(responseString); 

                    TempData["searchResults"] = properties; // Assuming you have logic to read these in PropertyListView 
                    return RedirectToPage("/PropertyListView"); 
                }
            }

            return BadRequest(); // Or handle the error more descriptively
        }
    }
}