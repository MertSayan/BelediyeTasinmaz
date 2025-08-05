using Application.Features.MediatR.Users.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Features.MediatR.Users.Handlers.Write
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IRepository<User> _repo;
        private readonly DefaultUserSettings _settings;

        public CreateUserCommandHandler(IRepository<User> repo, IOptions<DefaultUserSettings> options)
        {
            _repo = repo;
            _settings = options.Value;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                CreatedDate = DateTime.Now,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(_settings.InitialPassword) // default password hashed
            };

            await _repo.AddAsync(user);
            return user.UserId;
        }
    }
}
