namespace Order.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Models.Order> _orders = new();

        public IList<Models.Order> GetAll()
        {
            return _orders.Values.ToList();
        }

        public Models.Order? GetOrderById(Guid id)
        {
            return _orders[id];
        }

        public void CreateOrder(Models.Order? order)
        {
            if (order is null)
            {
                return;
            }

            _orders[order.Id] = order;
        }
    }
}
