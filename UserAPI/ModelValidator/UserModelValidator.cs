using FluentValidation;
using UserAPI.Model;

namespace UserAPI.ModelValidator
{
    public class UserModelValidator : AbstractValidator<PostUserRequestModel>
    {
        public UserModelValidator()
        {
            RuleFor(model => model.FirstName)
                .MaximumLength(10)
                .NotEmpty();

            RuleFor(model => model.MiddleName).MaximumLength(10);
            
            RuleFor(model => model.LastName).MaximumLength(10);
            
            RuleFor(model => model.PhoneNumber)
                .MinimumLength(10).WithMessage("Phone Number must be minimum 10 numbers")
                .MaximumLength(12).WithMessage("Phone number can be in the format of ###-##-#### ");
            
            RuleFor(model => model.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(msg => "Email address not valid");
            
        }
    }
}
