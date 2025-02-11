namespace ShoppinWeb.Services;

public interface IOrderingService
{
	[Get("/ordering-service/orders?pageNumber={pageNumber}&pageSize={pageSize}")]
	Task<GetOrdersResponse> GetOrders(int? pageNumber = 0, int? pageSize = 10);

	[Get("/ordering-service/orders/{orderName}")]
	Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

	[Get("/ordering-service/orders/customer/{customerId}")]
	Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
}
