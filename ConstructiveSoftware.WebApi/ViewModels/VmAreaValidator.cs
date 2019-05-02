using FluentValidation;

namespace ConstructiveSoftware.WebApi.ViewModels
{
	public class VmAreaValidator : AbstractValidator<VmArea>
	{
		public VmAreaValidator()
		{
			RuleFor(vm => vm.Name).NotEmpty().MaximumLength(200).WithMessage("Name cannot be empty");
		}
	}
}
