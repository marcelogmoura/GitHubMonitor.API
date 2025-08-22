using FluentValidation;
using GitHubMonitor.Domain.Dtos;

namespace GitHubMonitor.Domain.Validations
{
    public class GitHubRequestDtoValidation : AbstractValidator<GitHubRequestDto>
    {
        public GitHubRequestDtoValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("O nome de usuário do GitHub é obrigatório.");
        }
    }
}
