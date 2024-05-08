using iBurguer.Ordering.Core.Abstractions;

namespace iBurguer.Ordering.Core;

public static class Exceptions
{
    public class CannotToStartOrder() : DomainException<CannotToStartOrder>("Only orders in the 'Confirmed' state can initiate preparation.");

    public class CannotToConfirmOrder() : DomainException<CannotToConfirmOrder>("Only orders in the 'WaitingForPayment' state can be confirmed.");
    
    public class CannotToCompleteOrder() : DomainException<CannotToCompleteOrder>("Only orders in the 'In Progress' state can be completed for delivery.");
    
    public class CannotToDeliverOrder() : DomainException<CannotToDeliverOrder>("Only orders in the 'ReadyForPickup' state can be released for delivery.");
    
    public class CannotToCancelOrder() : DomainException<CannotToCancelOrder>("Only orders in the 'WaitingForPayment' or 'Confirmed' states can be canceled.");
    
    public class ThePickupCodeCannotBeEmptyOrNull() : DomainException<CannotToCancelOrder>("The pickup code cannot be null or empty.");

    public class InvalidOrderNumber() : DomainException<InvalidOrderNumber>("A value greater than zero must be provided for the order number.");
    
    public class InvalidPrice() : DomainException<InvalidPrice>("The price cannot have a value equal to zero or negative");
    
    public class InvalidQuantity() : DomainException<InvalidQuantity>("A value greater than zero must be provided for the quantity field.");

    public class OrderNotFound() : DomainException<OrderNotFound>("No order was found with the specified ID");

    public class OrderTrackingNotFound() : DomainException<OrderTrackingNotFound>("No order tracking was found the specified ID");
    
    public class ProductNameCannotBeNullOrEmpty() : DomainException<OrderTrackingNotFound>("Product name cannot be null or empty");

    public class LeastOneOrderItem() : DomainException<LeastOneOrderItem>("The order must have at least one item.");
}