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
			// Mapping from PropertyRequest to Properties
			CreateMap<PropertyRequest, Properties>()
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location)); // Map 'Location' to 'City'

			// Mapping from Properties to PropertiesViewModel
			CreateMap<Properties, PropertiesViewModel>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.City)) // Map 'City' back to 'Location'
				.ForMember(dest => dest.DisplayPrice, opt => opt.MapFrom(src => src.Price.ToString("PLN")))
				.ForMember(dest => dest.PropertyPhotos, opt => opt.MapFrom(src =>
					(src.OtherPics != null ? src.OtherPics.Split() : new string[] { })
					.ToList()
				));


			// Mapping from PropertyUpdateRequest to Properties
			CreateMap<PropertyUpdateRequest, Properties>()
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location)); // Map 'Location' to 'City'
        }
	}
}