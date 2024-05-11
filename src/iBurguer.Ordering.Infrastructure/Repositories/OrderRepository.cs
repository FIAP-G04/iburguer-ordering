using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace iBurguer.Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public DbSet<Order> Set => _context.Set<Order>();

    public async Task Save(Order order, CancellationToken cancellation)
    {
        await Set.AddAsync(order, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task Update(Order order, CancellationToken cancellation)
    {
        Set.Update(order);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<Order?> GetById(Guid orderId, CancellationToken cancellation)
    {
        return await Set.Include(o => o.Trackings).Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId, cancellation);
    }

    public async Task<int> GenerateOrderNumber(CancellationToken cancellation)
    {
        return await _context.Database.SqlQuery<int>($"SELECT NEXTVAL('sq_order_number') AS \"Value\"").FirstOrDefaultAsync(cancellation);
    }

    public async Task<PaginatedList<Order>> GetPagedOrders(int page, int limit, CancellationToken cancellation)
    {
        if (limit > 50) limit = 50;
        if (limit < 0) limit = 10;
        if (page < 1) page = 1;

        var query = _context.Orders
            .Include(o => o.Trackings)
            .Include(o => o.Items)
            .OrderByDescending(order => order.Number)
            .Skip((page - 1) * limit)
            .Take(limit);

        var total = await _context.Orders.CountAsync(cancellation);
        var paginatedData = await query.ToListAsync(cancellation);

        var paginatedList = new PaginatedList<Order>
        {
            Total = total,
            Page= page,
            Limit = limit,
            Items = paginatedData
        };

        return paginatedList;
    }
}