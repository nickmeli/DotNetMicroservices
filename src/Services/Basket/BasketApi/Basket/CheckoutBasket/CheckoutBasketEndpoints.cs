
using BasketApi.Basket.DeleteBasket;

namespace BasketApi.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
		{
			var command = request.Adapt<CheckoutBasketCommand>();

			var result = await sender.Send(command);

			var response = result.Adapt<CheckoutBasketResponse>();

			return Results.Ok(response);
		})
		.WithName("CheckoutBasket")
		.Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Checkout basket")
		.WithDescription("Checkout basket");
	}
}
