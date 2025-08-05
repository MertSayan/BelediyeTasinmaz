using Domain.Enums;
using MediatR;

namespace Application.Features.MediatR.Rentals.Commands
{
    public record ExtendRentalCommand(
        int RentalId,
        DateTime NewEndDate,
        decimal AdditionalAmount,
        PaymentFrequency? PaymentFrequencyOverride = null
    ) : IRequest<Unit>;
}
