using Application.Features.MediatR.Rentals.Results;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapperProfiles
{
    public class PaymentInstallmentProfile:Profile
    {
        public PaymentInstallmentProfile()
        {
            CreateMap<PaymentInstallment, InstallmentDto>();

        }
    }
}
