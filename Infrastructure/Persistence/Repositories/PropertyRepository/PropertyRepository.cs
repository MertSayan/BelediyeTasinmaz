using Application.Interfaces.PropertyInterface;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.PropertyRepository
{
    public class PropertyRepository:Repository<Property>,IPropertyRepository
    {
        private readonly HobiContext _context;
        public PropertyRepository(HobiContext context) : base(context)
        {
            _context = context;
        }

    }
}
