using Application.Interfaces.RentalInterface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Persistence.Context;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.RentalRepository
{
    public class RentalReportService : IRentalReportService
    {
        private readonly HobiContext _context;
        private readonly IWebHostEnvironment _env;

        public RentalReportService(HobiContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<string> GenerateReportAsync(int rentalId)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            // Kiralama bilgisi + ilişkili property + taksitler
            var rental = await _context.Rentals
                .Include(r => r.Property)
                .Include(r => r.PaymentInstallments)
                .FirstOrDefaultAsync(r => r.RentalId == rentalId);

            if (rental == null)
                throw new Exception("Kiralama bulunamadı.");

            // Rapor klasörü (wwwroot/reports)
            var folder = Path.Combine(_env.WebRootPath, "reports");
            Directory.CreateDirectory(folder);

            var fileName = $"rental-report-{rentalId}-{Guid.NewGuid().ToString().Substring(0, 8)}.pdf";
            var filePath = Path.Combine(folder, fileName);

            // PDF içeriği
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Content().Column(col =>
                    {
                        col.Item().Text("Kiralama Sözleşmesi").Bold().FontSize(18);

                        col.Item().Text($"Tarih: {rental.StartDate:dd.MM.yyyy} - {rental.EndDate:dd.MM.yyyy}");
                        col.Item().Text($"Taşınmaz: {rental.Property.Name} ({rental.Property.BlockNumber}/{rental.Property.ParcelNumber})");
                        col.Item().Text($"Tür: {rental.Property.Type}");
                        col.Item().Text($"Bölge: {rental.Property.Region}");
                        col.Item().Text($"Alan: {rental.Property.SizeSqm} m²");
                        col.Item().Text($"Kiracı: {rental.CitizenNationalId} | Tel: {rental.CitizenPhoneNumber}");

                        col.Item().Text($"Toplam Tutar: {rental.TotalAmount} ₺");
                        col.Item().Text($"Ödeme Periyodu: {rental.PaymentFrequency} | Taksit Sayısı: {rental.PaymentInstallments.Count}");

                        col.Item().Text("Taksitler:").Bold();
                        foreach (var i in rental.PaymentInstallments.OrderBy(p => p.DueDate))
                        {
                            col.Item().Text($"{i.DueDate:dd.MM.yyyy} - {i.Amount.ToString("N2", new CultureInfo("tr-TR"))} ₺");
                        }
                    });
                });
            }).GeneratePdf(filePath);

            rental.ReportPath = filePath;
            await _context.SaveChangesAsync();

            return fileName; // sadece dosya adı veya filePath => tam yol
        }
    }
}
