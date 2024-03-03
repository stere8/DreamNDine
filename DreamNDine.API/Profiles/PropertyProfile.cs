using AutoMapper;
using DreamNDine.BLL.Models;
using DreamNDine.API.Models;
using System.Linq;

namespace DreamNDine.API.Profiles
{
	public class PropertyProfile : Profile
	{
		public PropertyProfile()
		{
			// Mapping from PropertyRequest to Property
			CreateMap<PropertyRequest, Property>()
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location)); // Map 'Location' to 'City'

			// Mapping from Property to PropertiesViewModel
			CreateMap<Property, PropertiesViewModel>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.City)) // Map 'City' back to 'Location'
				.ForMember(dest => dest.DisplayPrice, opt => opt.MapFrom(src => src.Price.ToString("PLN")))
				.ForMember(dest => dest.PropertyPhotos, opt => opt.MapFrom(src => src.OtherPics.Split()));


			// Mapping from PropertyUpdateRequest to Property
			CreateMap<PropertyUpdateRequest, Property>()
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location)); // Map 'Location' to 'City'
		}
	}
}