using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace iBurguer.Orders.Infrastructure.Repositories;

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
        return await _context.Database.SqlQuery<int>($"SELECT NEXTVAL('sq_order_number') AS \"Value\"").FirstOrDefaultAsync();
    }
}