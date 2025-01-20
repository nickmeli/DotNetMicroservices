using OrderingApplication.Orders.Queries.GetOrderByName;
using OrderingApplication.Orders.Queries.GetOrdersByCustomer;

namespace OrderingApi.Endpoints;

/**
 * Accepts a name parameter
 * Constructs a GetOrdersByNameQuery 
 * Uses MadiatR to send the query to the corresponding handler
 * Retrievs the data and returns it
 */

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
		{
			var query = new GetOrderByNameQuery(orderName);

			var result = await sender.Send(query);

			var response = result.Adapt<GetOrdersByNameResponse>();

			return Results.Ok(response);
		})
		.WithName("GetOrdersByName")
		.Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.ProducesProblem(StatusCodes.Status404NotFound)
		.WithSummary("Get Orders By Name")
		.WithDescription("Get Orders By Name");
	}
}
