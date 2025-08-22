using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Helpers;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Services;

namespace GitHubMonitor.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AutenticarUsuarioResponseDto> AuthenticateUser(AutenticarUsuarioRequestDto request)
        {
            
            var user = _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);
                        
            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }
                        
            var token = JwtHelper.CreateToken(user);
                        
            return new AutenticarUsuarioResponseDto
            {
                Email = user.Email,
                Nome = user.Name,
                Token = token
            };
        }
    }
}