using Application.Features.MediatR.PaymentInstallments.Commands;
using Application.Interfaces.PaymentInstallmentInterface;
using MediatR;

namespace Application.Features.MediatR.PaymentInstallments.Handlers
{
    public class MarkInstallmentAsPaidCommandHandler : IRequestHandler<MarkInstallmentAsPaidCommand, bool>
    {
        private readonly IPaymentInstallmentRepository _installmentRepository;

        public MarkInstallmentAsPaidCommandHandler(IPaymentInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }

        public async Task<bool> Handle(MarkInstallmentAsPaidCommand request, CancellationToken cancellationToken)
        {
            var installment = await _installmentRepository.GetByIdAsync(request.InstallmentId);
            if (installment == null || installment.IsPaid)
                return false;

            installment.IsPaid = true;
            installment.PaidAt = DateTime.Now;
            if(installment.Notes != null)
            {
                installment.Notes = request.Notes;
            }
            else
            {
                installment.Notes = "Not Girilmedi";
            }

            await _installmentRepository.UpdateAsync(installment);
            return true;
        }
    }
}
