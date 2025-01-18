namespace OrderingDomain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;
