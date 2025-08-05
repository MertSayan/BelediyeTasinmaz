using Application.Interfaces.PaymentInstallmentInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Persistence.BackgroundServices
{
    public class PenaltyCalculatorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PenaltyCalculatorService> _logger;
        private const decimal PenaltyPerDay = 10m; // Günlük ceza sabiti (ileride konfigürasyona alınabilir)

        public PenaltyCalculatorService(IServiceProvider serviceProvider, ILogger<PenaltyCalculatorService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = DateTime.Today.AddDays(1).AddHours(3); // Ertesi gün 03:00
                var delay = nextRun - now;

                await Task.Delay(delay, stoppingToken);

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var installmentRepo = scope.ServiceProvider.GetRequiredService<IPaymentInstallmentRepository>();

                    var overdueInstallments = await installmentRepo.ListByFilterAsync(
                        filter: x => !x.IsPaid && x.DueDate < DateTime.Today,
                        orderByDescending: x => x.DueDate,
                        includes: x => x.Rental
                    );

                    var groupedByRental = overdueInstallments
                        .GroupBy(x => x.RentalId)
                        .ToList();

                    foreach (var rentalGroup in groupedByRental)
                    {
                        int overdueCount = rentalGroup.Count();

                        foreach (var installment in rentalGroup)
                        {
                            int daysOverdue = (DateTime.Today - installment.DueDate).Days;
                            decimal penalty = daysOverdue * PenaltyPerDay * overdueCount;

                            installment.OverdueDays = daysOverdue;
                            installment.TotalPenalty = penalty;

                            await installmentRepo.UpdateAsync(installment);
                        }
                    }

                    _logger.LogInformation($"[PenaltyCalculator] {DateTime.Now}: Ceza hesaplaması tamamlandı.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[PenaltyCalculator] Hata oluştu.");
                }

                //test sırasında 1 defa çalışsın sonra çıksın
                //break;
            }
        }
    }
}
