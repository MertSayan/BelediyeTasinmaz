using Application.Interfaces.PropertyInterface;
using Application.Interfaces.RentalInterface;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BackgroundServices
{
    public class RentalStatusBackgroundService:BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RentalStatusBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var rentalRepo = scope.ServiceProvider.GetRequiredService<IRentalRepository>();
                    var propertyRepo = scope.ServiceProvider.GetRequiredService<IPropertyRepository>();

                    var rentals = await rentalRepo.GetAllRentalForBackgroundService();
                    foreach (var rental in rentals)
                    {
                        if (rental.EndDate < DateTime.Now && rental.Property.Status == PropertyStatus.Rented)
                        {
                            rental.Property.Status = PropertyStatus.Available;
                            await propertyRepo.UpdateAsync(rental.Property);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken); // Her 1 saatte bir tekrar
            }
        }
    }
}
