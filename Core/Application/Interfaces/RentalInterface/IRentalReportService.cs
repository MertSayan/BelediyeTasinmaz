namespace Application.Interfaces.RentalInterface
{
    public interface IRentalReportService
    {
        //Task<string> GenerateReportAsync(int rentalId); // PDF oluşturur, dosya yolunu döner
        Task<string> GenerateReportAsync(int rentalId); // PDF byte olarak döner
        

    }
}
