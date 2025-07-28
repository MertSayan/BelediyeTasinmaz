using Application.Features.MediatR.Properties.Commands;
using Application.Features.MediatR.Properties.Results;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfiles
{
    public class PropertyProfile:Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, CreatePropertyCommand>().ReverseMap();
            CreateMap<Property, UpdatePropertyCommand>().ReverseMap();

            CreateMap<Property, GetAllPropertyByFilterQueryResult>()
          .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
          .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
          .ForMember(dest => dest.UpdatedByUserName, opt => opt.MapFrom(src => src.UpdatedByUser.FullName));
            
        }
    }
}
