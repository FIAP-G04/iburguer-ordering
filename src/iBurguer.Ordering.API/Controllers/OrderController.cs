using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.CompleteOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.DeliverOrder;
using iBurguer.Ordering.Core.UseCases.GetPagedOrders;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using iBurguer.Ordering.Core.UseCases.StartOrder;
using Microsoft.AspNetCore.Mvc;

namespace iBurguer.Ordering.API.Controllers;

/// <summary>
/// API controller for managing orders.
/// </summary>
[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    /// <summary>
    /// Retrieve a paginated list of orders
    /// </summary>
    /// <remarks>Retrieves a paginated list of orders from the system.</remarks>
    /// <param name="useCase">The use case responsible for retrieving the paginated list of orders.</param>
    /// <param name="page">The page number of the pagination. Defaults to 1.</param>
    /// <param name="limit">The maximum number of items per page. Defaults to 10.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <response code="200">Returns the paginated list of orders successfully.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response containing the paginated list of orders.</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedList<OrderSummaryResponse>), 200)]
    public async Task<IActionResult> GetPagedOrders([FromServices] IGetPagedOrdersUseCase useCase, int page = 1, int limit = 10, CancellationToken cancellationToken = default)
    {
        return Ok(await useCase.GetPagedOrders(page, limit, cancellationToken));
    }
    
    /// <summary>
    /// Cancel an existing order
    /// </summary>
    /// <remarks>Cancels an existing order in the system.</remarks>
    /// <param name="useCase">The use case responsible for canceling the order.</param>
    /// <param name="id">The ID of the order to be canceled.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Order canceled successfully.</response>
    /// <response code="404">Order not found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated order status.</returns>
    [HttpPatch("{id:guid}/canceled")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> CancelOrder([FromServices] ICancelOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.CancelOrder(id, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Complete an existing order
    /// </summary>
    /// <remarks>Marks an existing order as completed in the system.</remarks>
    /// <param name="useCase">The use case responsible for completing the order.</param>
    /// <param name="id">The ID of the order to be completed.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Order completed successfully.</response>
    /// <response code="404">Order not found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated order status.</returns>
    [HttpPatch("{id:guid}/completed")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> CompleteOrder([FromServices] ICompleteOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.CompleteOrder(id, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Confirm an existing order
    /// </summary>
    /// <remarks>Confirms an existing order in the system.</remarks>
    /// <param name="useCase">The use case responsible for confirming the order.</param>
    /// <param name="id">The ID of the order to be confirmed.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Order confirmed successfully.</response>
    /// <response code="404">Order not found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated order status.</returns>
    [HttpPatch("{id:guid}/confirmed")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> ConfirmOrder([FromServices] IConfirmOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.ConfirmOrder(id, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Deliver an existing order
    /// </summary>
    /// <remarks>Delivers an existing order in the system.</remarks>
    /// <param name="useCase">The use case responsible for delivering the order.</param>
    /// <param name="id">The ID of the order to be delivered.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Order delivered successfully.</response>
    /// <response code="404">Order not found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated order status.</returns>
    [HttpPatch("{id:guid}/delivered")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> DeliverOrder([FromServices] IDeliverOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.DeliverOrder(id, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Start processing an order
    /// </summary>
    /// <remarks>Starts processing an order in the system.</remarks>
    /// <param name="useCase">The use case responsible for starting the order processing.</param>
    /// <param name="id">The ID of the order to be processed.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <response code="200">Order processing started successfully.</response>
    /// <response code="404">Order not found.</response>
    /// <response code="500">Internal server error. Something went wrong on the server side.</response>
    /// <returns>Returns an HTTP response indicating the success of the operation along with the updated order status.</returns>
    [HttpPatch("{id:guid}/started")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> StartOrder([FromServices] IStartOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.StartOrder(id, cancellationToken);

        return Ok(response);
    }
}