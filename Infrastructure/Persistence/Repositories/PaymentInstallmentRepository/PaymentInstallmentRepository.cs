using Application.Interfaces.PaymentInstallmentInterface;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories.PaymentInstallmentRepository
{
    public class PaymentInstallmentRepository : Repository<PaymentInstallment>, IPaymentInstallmentRepository
    {
        private readonly HobiContext _context;
        public PaymentInstallmentRepository(HobiContext context) : base(context)
        {
             _context = context;
        }

        
    }
}
