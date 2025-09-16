using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Security;
using GitHubMonitor.Domain.Interfaces.Services;

namespace GitHubMonitor.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public Task<AutenticarUsuarioResponseDto> AuthenticateUser(AutenticarUsuarioRequestDto request)
        {
            var user = _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            var token = _jwtTokenService.GenerateToken(user);

            var response = new AutenticarUsuarioResponseDto
            {
                Email = user.Email,
                Nome = user.Name,
                Token = token
            };

            return Task.FromResult(response);
        }
    }
}