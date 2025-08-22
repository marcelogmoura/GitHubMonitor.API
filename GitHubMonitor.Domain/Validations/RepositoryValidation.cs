using FluentValidation;
using GitHubMonitor.Domain.Entities;

namespace GitHubMonitor.Domain.Validations
{
    public class RepositoryValidation : AbstractValidator<Repository>
    {
        public RepositoryValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("O ID do repositório é obrigatório.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do repositório é obrigatório.")
                .MaximumLength(150)
                .WithMessage("O nome do repositório não pode exceder 150 caracteres.");

            RuleFor(x => x.OwnerId)
                .NotNull()
                .WithMessage("O ID do proprietário do repositório é obrigatório.");
        }
    }
}
