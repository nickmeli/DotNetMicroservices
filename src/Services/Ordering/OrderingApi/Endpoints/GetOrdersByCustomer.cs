using OrderingApplication.Orders.Queries.GetOrdersByCustomer;

namespace OrderingApi.Endpoints;

/**
 * Accepts a customer ID
 * Constructs a GetOrdersByCustomerQuery
 * Uses MadiatR to send the query to the corresponding handler
 * Retrievs the data and returns it
 */

public record GetOrdersByCustomerIdRequest(Guid CustomerId);
public record GetOrdersByCustomerIdResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
		{
			var query = new GetOrdersByCustomerQuery(customerId);

			var result = await sender.Send(query);

			var response = result.Adapt<GetOrdersByCustomerIdResponse>();

			return Results.Ok(response);
		})
		.WithName("GetOrdersByCustomerId")
		.Produces<GetOrdersByCustomerIdResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.ProducesProblem(StatusCodes.Status404NotFound)
		.WithSummary("Get Orders By CustomerId")
		.WithDescription("Get Orders By CustomerId");
	}
}
