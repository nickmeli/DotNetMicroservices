﻿using Marten.Schema;

namespace BasketApi.Models;

public class ShoppingCart
{
	//[Identity]
	public string UserName { get; set; } = default!;
	public List<ShoppingCartItem> Items { get; set; } = new();
	public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

	public ShoppingCart(string userName)
	{
		UserName = userName;
	}

	// Required for Mapping
	public ShoppingCart()
	{
	}
}
