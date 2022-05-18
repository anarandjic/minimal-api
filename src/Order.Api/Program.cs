var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", (IOrderRepository repo) => repo.GetAll());

app.MapGet("/orders/{id}", (IOrderRepository repo, Guid id) =>
{
    var order = repo.GetOrderById(id);

    return order is not null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/oder", (IOrderRepository repo, Order order) =>
{
    repo.CreateOrder(order);

    return Results.Created($"/orders/{order.Id}", order);
});

app.Run();

public record Order
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; }
    public string MarketId { get; set; }
    public string StoreId { get; set; }
    public string Currency { get; set; }
}


public interface IOrderRepository
{
    IList<Order> GetAll();
    Order? GetOrderById(Guid id);
    void CreateOrder(Order? order);
}

public class OrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _orders = new();

    public IList<Order> GetAll()
    {
        return _orders.Values.ToList();
    }

    public Order? GetOrderById(Guid id)
    {
        return _orders[id];
    }

    public void CreateOrder(Order? order)
    {
        if (order is null)
        {
            return;
        }

        _orders[order.Id] = order;
    }
}