using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clients.Commands.Login
{
    internal class ClientLoginCommandHandler : IRequestHandler<ClientLoginCommand, string>
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService; 
        private readonly IApplicationDbContext _context;

        public ClientLoginCommandHandler(IPasswordService passwordService, ITokenService tokenService, IApplicationDbContext context)
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<string> Handle(ClientLoginCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Phone == request.Phone);

            if (client == null)
                throw new NotFoundException("Клиент", "Phone = " + request.Phone);

            if (!_passwordService.VerifyPasswordHash(request.Password, client.PasswordHash, client.PasswordSalt))
                throw new WrongPasswordException();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, client.Phone)
            };

            var accessToken = _tokenService.Create(claims);

            return accessToken;
        }
    }
}
