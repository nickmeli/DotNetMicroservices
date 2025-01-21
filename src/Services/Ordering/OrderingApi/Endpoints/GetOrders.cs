using OrderingApplication.Orders.Queries.GetOrders;

namespace OrderingApi.Endpoints;

/**
 * Accepts pagination parameters
 * Constructs a GetOrdersQuery with parameters
 * Uses MadiatR to send the query to the corresponding handler
 * Retrievs the data and returns it in a paginated format
 */

public record GetOrdersRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
		{
			var result = await sender.Send(new GetOrdersQuery(request));

			var response = result.Adapt<GetOrdersResponse>();

			return Results.Ok(response);
		})
		.WithName("GetOrders")
		.Produces<GetOrdersResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Orders")
		.WithDescription("Get Orders");
	}
}
