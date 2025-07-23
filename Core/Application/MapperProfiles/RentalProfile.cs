using Application.Features.MediatR.Rentals.Commands;
using Application.Features.MediatR.Rentals.Results;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfiles
{
    public class RentalProfile:Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, CreateRentalCommand>().ReverseMap();

            CreateMap<Rental, GetRentalsQueryResult>()
           .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name))
           .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.Property.Type.ToString()))
           .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Property.Region))
           .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.PaymentInstallments))
           .ForMember(dest => dest.CreatedEmployee, opt => opt.MapFrom(src => src.CreatedByUser.FullName));

            CreateMap<Rental, GetRentalHistoryByPropertyIdResult>()
           .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.PaymentInstallments));
        }
    }
}
