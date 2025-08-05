using Application.Features.MediatR.Rentals.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RentalInterface
{
    public interface IRentalStatisticRepository
    {
        Task<GetRentalStatisticsQueryResult> GetStatisticsAsync();
    }
}
