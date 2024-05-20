using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Abstractions;

namespace iBurguer.Ordering.Core;

[ExcludeFromCodeCoverage]
public static class Exceptions
{
    public class CannotToStartOrderException() : DomainException<CannotToStartOrderException>("Only orders in the 'Confirmed' state can initiate preparation.");

    public class CannotToConfirmOrderException() : DomainException<CannotToConfirmOrderException>("Only orders in the 'WaitingForPayment' state can be confirmed.");
    
    public class CannotToCompleteOrderException() : DomainException<CannotToCompleteOrderException>("Only orders in the 'In Progress' state can be completed for delivery.");
    
    public class CannotToDeliverOrderException() : DomainException<CannotToDeliverOrderException>("Only orders in the 'ReadyForPickup' state can be released for delivery.");
    
    public class CannotToCancelOrderException() : DomainException<CannotToCancelOrderException>("Only orders in the 'WaitingForPayment' or 'Confirmed' states can be canceled.");
    
    public class ThePickupCodeCannotBeEmptyOrNullException() : DomainException<ThePickupCodeCannotBeEmptyOrNullException>("The pickup code cannot be null or empty.");

    public class InvalidOrderNumberException() : DomainException<InvalidOrderNumberException>("A value greater than zero must be provided for the order number.");
    
    public class InvalidPriceException() : DomainException<InvalidPriceException>("The price cannot have a value equal to zero or negative");
    
    public class InvalidQuantityException() : DomainException<InvalidQuantityException>("A value greater than zero must be provided for the quantity field.");

    public class OrderNotFoundException() : DomainException<OrderNotFoundException>("No order was found with the specified ID");

    public class OrderTrackingNotFoundException() : DomainException<OrderTrackingNotFoundException>("No order tracking was found the specified ID");
    
    public class ProductNameCannotBeNullOrEmptyException() : DomainException<OrderTrackingNotFoundException>("Product name cannot be null or empty");

    public class LeastOneOrderItemException() : DomainException<LeastOneOrderItemException>("The order must have at least one item.");
}