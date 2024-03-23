using AutoMapper;
using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DreamNDine.API.Models;

namespace DreamNDine.Api.Controllers
{
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



        // GET: api/Properties
        [HttpGet]
        public IEnumerable<PropertiesViewModel> GetAllProperties()
        {
            Console.WriteLine("GetAllProperties");
            return _housingService.GetAllProperties()
                                  .Select(p => _mapper.Map<PropertiesViewModel>(p));
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public ActionResult<PropertiesViewModel> GetPropertyById(int id)
        {
            var property = _housingService.GetPropertyById(id);
            if (property == null) return NotFound();
            return _mapper.Map<PropertiesViewModel>(property);
        }

        // POST: api/Properties/search/
        [HttpPost("search")]
        public IEnumerable<PropertiesViewModel> GetPropertiesByCityAndTime(PropertySearchModel searchModel)
        {
            return _housingService.GetPropertiesByCityAndTime(searchModel.location, searchModel.checkInDate, searchModel.checkOutDate)
                                  .Select(p => _mapper.Map<PropertiesViewModel>(p));
        }

        // POST: api/Properties
        [HttpPost]
        public ActionResult<PropertiesViewModel> CreateProperty(PropertyRequest propertyRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var property = _mapper.Map<Properties>(propertyRequest);

            // Assuming authentication sets the owner ID
            property.OwnerID = GetCurrentUserId();

            var createdProperty = _housingService.AddProperty(property);

            return CreatedAtAction("GetPropertyById", new { id = createdProperty.PropertyID }, _mapper.Map<PropertiesViewModel>(createdProperty));
        }

        // PUT: api/Properties/5
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, PropertyUpdateRequest propertyUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Ensure the user is authorized to update this properties
            if (GetCurrentUserId() != _housingService.GetPropertyOwner(id))
            {
                return Unauthorized();
            }

            try
            {
                var propertyToUpdate = _housingService.GetPropertyById(id); // Will now only be called if the user is authorized
                if (propertyToUpdate == null) return NotFound();

                _mapper.Map(propertyUpdateRequest, propertyToUpdate);
                _housingService.EditProperty(propertyToUpdate);
                return NoContent();
            }
            catch (PropertyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Properties/5
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