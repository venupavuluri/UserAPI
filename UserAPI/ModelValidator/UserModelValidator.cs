using FluentValidation;
using UserAPI.Model;

namespace UserAPI.ModelValidator
{
    public class UserModelValidator : AbstractValidator<PostUserRequestModel>
    {
        public UserModelValidator()
        {
            RuleFor(model => model.EmailAddress).MaximumLength(10);
            RuleFor(model => model.FirstName).MaximumLength(10);
            RuleFor(model => model.MiddleName).MaximumLength(10);
            RuleFor(model => model.LastName).MaximumLength(10);
            RuleFor(m => m.EmailAddress).NotEmpty();
        }
    }
}
