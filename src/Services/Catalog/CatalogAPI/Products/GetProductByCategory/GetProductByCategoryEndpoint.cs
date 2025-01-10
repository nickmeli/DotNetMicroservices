﻿
using CatalogAPI.Products.GetProductById;

namespace CatalogAPI.Products.GetProductByCategory;

// public record GetProductsByCategoryRequest();
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
		{
			var result = await sender.Send(new GetProductsByCategoryQuery(category));

			var response = result.Adapt<GetProductsByCategoryResponse>();

			return Results.Ok(response);
		})
		.WithName("GetProductsByCategory")
		.Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Products by Category")
		.WithDescription("Get Products by Category");
	}
}
