using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels.Systems
{
    public class UserCreatRequesrValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreatRequesrValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format is not match");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name can not over 50 character limit");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name can not over 50 character limit");
        }
    }
}
