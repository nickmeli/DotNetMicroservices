using MassTransit;
using Microsoft.FeatureManagement;

namespace OrderingApplication.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
		ILogger<OrderCreatedEventHandler> logger,
		IPublishEndpoint publishEndpoint,
		IFeatureManager featureManager
	)
	: INotificationHandler<OrderCreatedEvent>
{
	public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
	{
		logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

		if (await featureManager.IsEnabledAsync("OrderFullfilment"))
		{
			var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
			await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
		}
	}
}
