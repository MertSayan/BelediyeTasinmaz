using Application.Features.MediatR.Rentals.Results;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfiles
{
    public class PaymentInstallmentProfile:Profile
    {
        public PaymentInstallmentProfile()
        {
            CreateMap<PaymentInstallment, InstallmentDto>();
            CreateMap<PaymentInstallment, PaymentInstallmentResult>();


        }
    }
}
