using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Coachs.Commands.Login
{
    internal class CoachLoginCommandHandler : IRequestHandler<CoachLoginCommand, string>
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IApplicationDbContext _context;

        public CoachLoginCommandHandler(IPasswordService passwordService, ITokenService tokenService, IApplicationDbContext context)
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<string> Handle(CoachLoginCommand request, CancellationToken cancellationToken)
        {
            var coach = await _context.Coachs.FirstOrDefaultAsync(x => x.Phone == request.Phone);

            if (coach == null)
            {
                throw new NotFoundException("Тренер", "Phone = " + request.Phone);
            }

            if (!_passwordService.VerifyPasswordHash(request.Password, coach.PasswordHash, coach.PasswordSalt))
            {
                throw new WrongPasswordException();
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, coach.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, request.Phone)
            };

            var accessToken = _tokenService.Create(claims);

            return accessToken;
        }
    }
}
