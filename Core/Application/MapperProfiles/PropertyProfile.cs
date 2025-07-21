using Application.Features.MediatR.Properties.Commands;
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
        }
    }
}
