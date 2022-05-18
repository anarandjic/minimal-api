namespace Order.Api.Models
{
    public record Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string MarketId { get; set; }
        public string StoreId { get; set; }
        public string Currency { get; set; }
    }
}
