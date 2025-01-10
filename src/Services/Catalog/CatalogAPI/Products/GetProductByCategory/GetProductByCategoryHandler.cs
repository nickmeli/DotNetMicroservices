﻿
using CatalogAPI.Products.GetProductById;

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(
	IDocumentSession session
) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
	public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
	{
		var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

		return new GetProductsByCategoryResult(products);
	}
}
