using iBurguer.Orders.Core.UseCases;
using iBurguer.Orders.Core.UseCases.CancelOrder;
using iBurguer.Orders.Core.UseCases.CompleteOrder;
using iBurguer.Orders.Core.UseCases.ConfirmOrder;
using iBurguer.Orders.Core.UseCases.DeliverOrder;
using iBurguer.Orders.Core.UseCases.RegisterOrder;
using iBurguer.Orders.Core.UseCases.StartOrder;
using Microsoft.AspNetCore.Mvc;

namespace iBurguer.Orders.API.Controllers;

/// <summary>
/// API controller for managing orders.
/// </summary>
[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterOrderResponse), 201)]
    public async Task<ActionResult> RegisterOrder([FromServices] IRegisterOrderUseCase useCase, RegisterOrderRequest request, CancellationToken cancellationToken = default)
    {
        var response = await useCase.RegisterOrder(request, cancellationToken);

        return Created("Order created successfully", response);
    }
    
    [HttpPatch("{id:guid}/canceled")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> CancelOrder([FromServices] ICancelOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.CancelOrder(id, cancellationToken);

        return Ok(response);
    }
    
    [HttpPatch("{id:guid}/completed")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> CompleteOrder([FromServices] ICompleteOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.CompleteOrder(id, cancellationToken);

        return Ok(response);
    }
    
    [HttpPatch("{id:guid}/confirmed")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> ConfirmOrder([FromServices] IConfirmOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.ConfirmOrder(id, cancellationToken);

        return Ok(response);
    }
    
    [HttpPatch("{id:guid}/delivered")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> DeliverOrder([FromServices] IDeliverOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.DeliverOrder(id, cancellationToken);

        return Ok(response);
    }
    
    [HttpPatch("{id:guid}/started")]
    [ProducesResponseType(typeof(OrderStatusResponse), 200)]
    public async Task<ActionResult> StartOrder([FromServices] IStartOrderUseCase useCase, Guid id, CancellationToken cancellationToken = default)
    {
        var response = await useCase.StartOrder(id, cancellationToken);

        return Ok(response);
    }
}