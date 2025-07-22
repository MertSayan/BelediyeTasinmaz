using Application.Interfaces.UserInterface;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly HobiContext _context;
        public UserRepository(HobiContext context) : base(context)
        {
            _context = context;
        }
    }
}
