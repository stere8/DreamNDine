using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DreamNDine.BLL.Services;
using DreamNDine.BLL.Models;
using System.Security.Claims;

namespace DreamNDine.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IHousingService _housingService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PropertyController(IHttpContextAccessor httpContextAccessor, IHousingService housingService, IMapper mapper)
        {
            _housingService = housingService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Property
        [HttpGet]
        public IEnumerable<PropertyViewModel> GetAllProperties()
        {
            return _housingService.GetAllProperties()
                                  .Select(p => _mapper.Map<PropertyViewModel>(p));
        }

        // GET: api/Property/5
        [HttpGet("{id}")]
        public ActionResult<PropertyViewModel> GetPropertyById(int id)
        {
            var property = _housingService.GetPropertyById(id);
            if (property == null) return NotFound();
            return _mapper.Map<PropertyViewModel>(property);
        }

        // GET: api/Property/search?city=...&startDate=...&endDate=...
        [HttpGet("search")]
        public IEnumerable<PropertyViewModel> GetPropertiesByCityAndTime(string city, DateTime startDate, DateTime endDate)
        {
            return _housingService.GetPropertiesByCityAndTime(city, startDate, endDate)
                                  .Select(p => _mapper.Map<PropertyViewModel>(p));
        }

        // POST: api/Property
        [HttpPost]
        public ActionResult<PropertyViewModel> CreateProperty(PropertyRequest propertyRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var property = _mapper.Map<Property>(propertyRequest);

            // Assuming authentication sets the owner ID
            property.OwnerID = GetCurrentUserId();

            var createdProperty = _housingService.AddProperty(property);

            return CreatedAtAction("GetPropertyById", new { id = createdProperty.PropertyID }, _mapper.Map<PropertyViewModel>(createdProperty));
        }

        // PUT: api/Property/5
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, PropertyUpdateRequest propertyUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var propertyToUpdate = _housingService.GetPropertyById(id);
            if (propertyToUpdate == null) return NotFound();

            _mapper.Map(propertyUpdateRequest, propertyToUpdate); // Update properties 

            if (propertyToUpdate.OwnerID != GetCurrentUserId())
            {
                return Unauthorized();
            }

            _housingService.EditProperty(propertyToUpdate);
            return NoContent();
        }

        // DELETE: api/Property/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = _housingService.GetPropertyById(id);
            if (property == null) return NotFound();

            if (property.OwnerID != GetCurrentUserId())
            {
                return Unauthorized();
            }

            _housingService.DeleteProperty(property.PropertyID);
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Convert.ToInt32(userId);
        }
    }
}
