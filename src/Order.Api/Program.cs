var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", (OrderRepository repo) => repo.GetAll());

app.MapGet("/orders/{id}", (OrderRepository repo, Guid id) =>
{
    var order = repo.GetOrderById(id);

    return order is not null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/oder", (OrderRepository repo, Order order) =>
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

public class OrderRepository
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