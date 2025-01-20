using FluentValidation;

namespace OrderingApplication.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId)
	: ICommand<DeleteOrderResponse>;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
	public DeleteOrderCommandValidator()
	{
		RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
	}
}
