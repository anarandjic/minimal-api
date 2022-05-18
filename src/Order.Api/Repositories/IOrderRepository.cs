namespace Order.Api.Repositories
{
    public interface IOrderRepository
    {
        IList<Models.Order> GetAll();
        Models.Order? GetOrderById(Guid id);
        void CreateOrder(Models.Order? order);
    }

}
