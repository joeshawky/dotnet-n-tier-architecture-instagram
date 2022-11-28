using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.FluentValidation;

public class UserValidator : AbstractValidator<User>, IUserValidator
{
    public UserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Username).MinimumLength(3).WithMessage("Minimum length is 3");
        RuleFor(x => x.Username).MaximumLength(20).WithMessage("Maximum length is 20");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.Password).MaximumLength(20).WithMessage("Maximum length is 20");
        RuleFor(x => x.Password).MaximumLength(20).WithMessage("Maximum length is 20");

    }
}