using Application.Constans;
using Application.Features.MediatR.Users.Commands;
using Application.Interfaces;
using Application.Interfaces.TokenInterface;
using Domain.Entities;
using MediatR;

namespace Application.Features.MediatR.Users.Handlers.Write
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public LoginCommandHandler(IRepository<User> userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilterAsync(x=>x.Email==request.Email);
            if(user == null)
            {
                throw new Exception(Messages<User>.EntityNotFound);
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordMatch)
                throw new Exception(Messages<User>.PasswordNotCorrect);

            var token = _tokenRepository.GenerateToken(user);
            return token;
        }
    }
}
