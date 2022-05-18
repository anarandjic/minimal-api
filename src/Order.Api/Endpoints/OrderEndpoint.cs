using Order.Api.Repositories;

namespace Order.Api.Endpoints
{
    public static class OrderEndpoint
    {
        public static void MapOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/orders", GetAllOrders);
            app.MapGet("/orders/{id}", GetOrderById);
            app.MapPost("/orders", CreateOrder);

            #region todo
            //app.MapPost("/orders", CreateOrder).WithValidator<Order>();
            #endregion
        }

        private static IResult CreateOrder(IOrderRepository repo, Models.Order order)
        {
            repo.CreateOrder(order);

            return Results.Created($"/orders/{order.Id}", order);
        }

        private static IList<Models.Order> GetAllOrders(IOrderRepository repo)
        {
            return repo.GetAll();
        }

        private static IResult GetOrderById(IOrderRepository repo, Guid id)
        {
            var order = repo.GetOrderById(id);

            return order is not null ? Results.Ok(order) : Results.NotFound();
        }
    }
}
