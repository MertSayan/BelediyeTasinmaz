using Application.Features.MediatR.Rentals.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfiles
{
    public class RentalProfile:Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, CreateRentalCommand>().ReverseMap();
        }
    }
}
