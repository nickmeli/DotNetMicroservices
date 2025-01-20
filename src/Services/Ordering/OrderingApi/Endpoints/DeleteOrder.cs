using OrderingApplication.Orders.Commands.CreateOrder;
using OrderingApplication.Orders.Commands.DeleteOrder;

namespace OrderingApi.Endpoints;

/**
 * Accepts the orderID as a parameter
 * Constructs a DeleteOrderCommand
 * Uses MadiatR to send the command to the corresponding handler
 * Returns a success or not found response
 */

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
		{
			var command = new DeleteOrderCommand(orderId);

			var result = await sender.Send(command);

			var response = result.Adapt<CreateOrderResponse>();

			return Results.Ok(response);
		})
		.WithName("DeleteOrder")
		.Produces<CreateOrderResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Delete Order")
		.WithDescription("Delete Order");
	}
}
