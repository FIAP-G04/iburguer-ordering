using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases.GetPagedOrders;

public interface IGetPagedOrdersUseCase
{
    Task<PaginatedList<OrderSummaryResponse>> GetPagedOrders(int page, int limit, CancellationToken cancellationToken);
}

[ExcludeFromCodeCoverage]
public class GetPagedOrdersUseCase : IGetPagedOrdersUseCase
{
    private readonly IOrderRepository _repository;

    public GetPagedOrdersUseCase(IOrderRepository repository) => _repository = repository;

    public async Task<PaginatedList<OrderSummaryResponse>> GetPagedOrders(int page = 1, int limit = 10, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetPagedOrders(page, limit, cancellationToken);

        return new PaginatedList<OrderSummaryResponse>
        {
            Page = result.Page,
            Limit = result.Limit,
            Total = result.Total,
            Items = result.Items.Select(order => OrderSummaryResponse.Convert(order))
        };
    }
}