using Application.Interfaces.RentalInterface;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.RentalRepository
{
    public class RentalRepository:Repository<Rental>,IRentalRepository
    {
        private readonly HobiContext _context;
        public RentalRepository(HobiContext context) : base(context)
        {
            _context = context;
        }

    }
}
