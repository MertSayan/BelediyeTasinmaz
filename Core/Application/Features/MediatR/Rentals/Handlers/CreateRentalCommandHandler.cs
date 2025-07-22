using Application.Features.MediatR.Rentals.Commands;
using MediatR;

namespace Application.Features.MediatR.Rentals.Handlers
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Unit>
    {

        public Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
